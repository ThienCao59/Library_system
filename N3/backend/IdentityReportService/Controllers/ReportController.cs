using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IdentityReportService.Data;
using IdentityReportService.DTOs;
using IdentityReportService.Services;

namespace IdentityReportService.Controllers
{
    [Authorize(Policy = "RequireAdminOrLibrarian")]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IEventLogService _eventLogService;

        /// <summary>Chuông thông báo — chỉ cảnh báo cần xử lý (không gồm audit đăng nhập, CRUD...).</summary>
        private static readonly HashSet<string> AlertEventTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            "circulation.borrow.requested",
            "circulation.return.requested",
            "circulation.renewal.requested",
            "circulation.overdue",
            "card.expiring",
            "document.pending",
            "system.error",
        };

        public ReportController(
            AppDbContext context,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IEventLogService eventLogService)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _eventLogService = eventLogService;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardReport()
        {
            var totalUsers = await _context.Users.CountAsync(u => u.Role == "Reader");
            var activeUsers = await _context.Users.CountAsync(u => u.Role == "Reader" && u.IsActive);
            var inactiveUsers = totalUsers - activeUsers;

            var userStats = new UserStatsDto
            {
                TotalUsers = totalUsers,
                ActiveUsers = activeUsers,
                InactiveUsers = inactiveUsers
            };

            var totalCards = await _context.LibraryCards.CountAsync(c => c.User.Role == "Reader");
            var activeCards = await _context.LibraryCards.CountAsync(c => c.User.Role == "Reader" && c.Status == "Active");
            var lockedCards = await _context.LibraryCards.CountAsync(c => c.User.Role == "Reader" && c.Status == "Locked");
            var expiredCards = await _context.LibraryCards.CountAsync(c => c.User.Role == "Reader" && c.Status == "Expired");

            var cardStats = new CardStatsDto
            {
                TotalCards = totalCards,
                ActiveCards = activeCards,
                LockedCards = lockedCards,
                ExpiredCards = expiredCards
            };

            var weeklyCardStats = new List<WeeklyCardDto>();
            var now = DateTime.UtcNow;
            for (int i = 7; i >= 0; i--)
            {
                var weekStart = now.AddDays(-7 * i - (int)now.DayOfWeek);
                var weekEnd = weekStart.AddDays(7);
                var activeCount = await _context.LibraryCards.CountAsync(c => c.User.Role == "Reader" && c.Status == "Active" && c.IssueDate <= weekEnd);
                var lockedCount = await _context.LibraryCards.CountAsync(c => c.User.Role == "Reader" && c.Status == "Locked" && c.IssueDate <= weekEnd);

                weeklyCardStats.Add(new WeeklyCardDto
                {
                    Week = $"W{8 - i}",
                    ActiveCards = activeCount,
                    LockedCards = lockedCount
                });
            }

            var gatewayUrl = _configuration["ApiGateway:Url"] ?? "http://localhost:5000";

            var monthlyBorrowStatsTask = Task.Run(async () =>
            {
                try
                {
                    var client = CreateGatewayClient();
                    client.Timeout = TimeSpan.FromSeconds(3);
                    return await client.GetFromJsonAsync<List<MonthlyBorrowDto>>($"{gatewayUrl}/api/circulation/stats/monthly") ?? new List<MonthlyBorrowDto>();
                }
                catch
                {
                    return new List<MonthlyBorrowDto>();
                }
            });

            var topPopularBooksTask = Task.Run(async () =>
            {
                try
                {
                    var client = CreateGatewayClient();
                    client.Timeout = TimeSpan.FromSeconds(3);
                    return await client.GetFromJsonAsync<List<PopularBookDto>>($"{gatewayUrl}/api/catalog/stats/popular") ?? new List<PopularBookDto>();
                }
                catch
                {
                    return new List<PopularBookDto>();
                }
            });

            await Task.WhenAll(monthlyBorrowStatsTask, topPopularBooksTask);
            var monthlyBorrowStats = await monthlyBorrowStatsTask;
            var topPopularBooks = await topPopularBooksTask;

            var dashboardReport = new DashboardReportDto
            {
                UserStats = userStats,
                CardStats = cardStats,
                MonthlyBorrowStats = monthlyBorrowStats,
                TopPopularBooks = topPopularBooks,
                WeeklyCardStats = weeklyCardStats
            };

            return Ok(dashboardReport);
        }

        /// <summary>Chuông thông báo — chỉ cảnh báo vận hành cần xử lý.</summary>
        [HttpGet("notifications")]
        public async Task<IActionResult> GetNotifications()
        {
            var notifications = new List<NotificationItemDto>();
            var now = DateTime.UtcNow;

            var recentAlerts = await _context.EventLogs
                .Where(e => AlertEventTypes.Contains(e.EventType))
                .OrderByDescending(e => e.Timestamp)
                .Take(40)
                .ToListAsync();

            notifications.AddRange(recentAlerts.Select(log => new NotificationItemDto
            {
                Id = log.Id,
                EventType = log.EventType,
                Data = log.Data,
                Timestamp = log.Timestamp,
                SourceService = log.SourceService
            }));

            var expiringCards = await _context.LibraryCards
                .Include(c => c.User)
                .Where(c => c.User.Role == "Reader"
                    && c.Status == "Active"
                    && c.ExpiryDate > now
                    && c.ExpiryDate <= now.AddDays(30))
                .OrderBy(c => c.ExpiryDate)
                .Take(10)
                .ToListAsync();

            foreach (var card in expiringCards)
            {
                notifications.Add(new NotificationItemDto
                {
                    Id = card.Id,
                    EventType = "card.expiring",
                    Data = JsonSerializer.Serialize(new
                    {
                        cardNumber = card.CardNumber,
                        ownerName = card.User.FullName,
                        expiryDate = card.ExpiryDate
                    }),
                    Timestamp = card.ExpiryDate,
                    SourceService = "IdentityReportService"
                });
            }

            notifications.AddRange(await FetchCirculationLogsAsync(30, AlertEventTypes));

            var deduped = notifications
                .GroupBy(n => $"{n.EventType}|{n.Data}|{n.Timestamp:O}")
                .Select(g => g.First())
                .Where(n => AlertEventTypes.Contains(n.EventType))
                .OrderByDescending(n => n.Timestamp)
                .Take(30)
                .ToList();

            return Ok(deduped);
        }

        /// <summary>Nhật ký hệ thống — phân trang, gộp EventLogs + mượn/trả từ N2.</summary>
        [HttpGet("activity-logs")]
        public async Task<IActionResult> GetActivityLogs(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? type = null)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 10, 500);

            var filter = string.IsNullOrWhiteSpace(type) ? "all" : type.Trim().ToLowerInvariant();
            var logs = new List<NotificationItemDto>();
            var needsCirculation = filter is "all" or "circulation";
            var needsEventLogs = filter is not "circulation";

            if (needsEventLogs)
            {
                var eventLogs = await _context.EventLogs
                    .OrderByDescending(e => e.Timestamp)
                    .Take(1000)
                    .ToListAsync();

                logs.AddRange(eventLogs.Select(log => new NotificationItemDto
                {
                    Id = log.Id,
                    EventType = log.EventType,
                    Data = log.Data,
                    Timestamp = log.Timestamp,
                    SourceService = log.SourceService
                }));
            }

            if (needsCirculation)
                logs.AddRange(await FetchCirculationLogsAsync(500, null));

            var merged = logs
                .GroupBy(l => $"{l.EventType}|{l.Data}|{l.Timestamp:O}|{l.Id}")
                .Select(g => g.First())
                .Where(l => MatchesActivityFilter(l.EventType, filter))
                .OrderByDescending(l => l.Timestamp)
                .ToList();

            var total = merged.Count;
            var items = merged
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new ActivityLogsPageDto
            {
                Items = items,
                Total = total,
                Page = page,
                PageSize = pageSize
            });
        }

        /// <summary>N1/N2 gửi sự kiện (thêm sách, mượn/trả...) vào nhật ký.</summary>
        [HttpPost("events")]
        public async Task<IActionResult> IngestEvent([FromBody] IngestEventDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _eventLogService.LogAsync(
                dto.EventType,
                dto.Data,
                dto.SourceService ?? "ExternalService");

            return Ok(new { Message = "Event logged." });
        }

        private HttpClient CreateGatewayClient()
        {
            var client = _httpClientFactory.CreateClient();
            var authHeader = Request.Headers.Authorization.ToString();
            if (!string.IsNullOrWhiteSpace(authHeader))
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authHeader);
            return client;
        }

        private async Task<List<NotificationItemDto>> FetchCirculationLogsAsync(int take, HashSet<string>? allowedTypes)
        {
            var logs = new List<NotificationItemDto>();
            var gatewayUrl = _configuration["ApiGateway:Url"] ?? "http://localhost:5000";

            try
            {
                var client = CreateGatewayClient();
                client.Timeout = TimeSpan.FromSeconds(5);
                var txResponse = await client.GetAsync($"{gatewayUrl}/api/circulation/transactions?pageSize={Math.Clamp(take, 10, 200)}");
                if (!txResponse.IsSuccessStatusCode)
                    return logs;

                var json = await txResponse.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                if (!TryGetTransactionItems(doc.RootElement, out var items))
                    return logs;

                foreach (var tx in items)
                {
                    var status = GetJsonString(tx, "status", "Status")?.Trim().ToLowerInvariant() ?? "";
                    var bookTitle = GetJsonString(tx, "tenSach", "TenSach", "bookTitle") ?? "Sách";
                    var cardNumber = GetJsonString(tx, "cardNumber", "CardNumber");
                    var txId = GetJsonString(tx, "id", "Id");
                    
                    var createdAt = GetJsonDateTime(tx, "createdAt", "CreatedAt", "requestDate", "RequestDate", "borrowDate", "BorrowDate");
                    var updatedAt = GetJsonDateTime(tx, "updatedAt", "UpdatedAt", "returnDate", "ReturnDate");

                    var baseId = Guid.TryParse(txId, out var parsedId) ? parsedId : Guid.NewGuid();
                    var dataStr = JsonSerializer.Serialize(new { bookTitle, cardNumber, status });

                    // 1. Always emit the initial event (Borrow Request or Borrowed) based on createdAt
                    var initialTime = createdAt ?? updatedAt ?? DateTime.UtcNow;
                    var initialEventType = status == "pending" ? "circulation.borrow.requested" : "circulation.borrowed";

                    if (allowedTypes == null || allowedTypes.Contains(initialEventType))
                    {
                        logs.Add(new NotificationItemDto
                        {
                            Id = baseId,
                            EventType = initialEventType,
                            Data = dataStr,
                            Timestamp = initialTime,
                            SourceService = "CirculationService"
                        });
                    }

                    // 2. If the book progressed beyond just being borrowed, emit a SECOND event based on updatedAt
                    if (status is "returned" or "overdue" or "returnpending" or "renewalpending" or "renewpending")
                    {
                        var secondEventType = status switch
                        {
                            "returned" => "circulation.returned",
                            "overdue" => "circulation.overdue",
                            "returnpending" => "circulation.return.requested",
                            "renewalpending" or "renewpending" => "circulation.renewal.requested",
                            _ => "circulation.updated"
                        };

                        var secondTime = updatedAt ?? initialTime;

                        if (allowedTypes == null || allowedTypes.Contains(secondEventType))
                        {
                            logs.Add(new NotificationItemDto
                            {
                                Id = Guid.NewGuid(), // Different ID to prevent deduping
                                EventType = secondEventType,
                                Data = dataStr,
                                Timestamp = secondTime,
                                SourceService = "CirculationService"
                            });
                        }
                    }
                }
            }
            catch
            {
                // Circulation offline — identity logs vẫn trả về
            }

            return logs;
        }

        private static bool MatchesActivityFilter(string eventType, string filter)
        {
            if (filter is "all" or "")
                return true;

            return filter switch
            {
                "auth" => eventType is "user.login" or "user.logout" or "user.registered",
                "user" => (eventType.StartsWith("user.", StringComparison.OrdinalIgnoreCase)
                        && eventType is not ("user.login" or "user.logout" or "user.registered"))
                    || eventType.StartsWith("card.", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(eventType, "staff.created", StringComparison.OrdinalIgnoreCase),
                "book" => eventType.StartsWith("book.", StringComparison.OrdinalIgnoreCase),
                "circulation" => eventType.StartsWith("circulation.", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(eventType, "document.pending", StringComparison.OrdinalIgnoreCase),
                _ => true
            };
        }

        private static string MapTransactionEventType(string? status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return "circulation.updated";

            return status.Trim().ToLowerInvariant() switch
            {
                "pending" => "circulation.borrow.requested",
                "returnpending" => "circulation.return.requested",
                "borrowed" => "circulation.borrowed",
                "returned" => "circulation.returned",
                "overdue" => "circulation.overdue",
                "renewalpending" or "renewpending" => "circulation.renewal.requested",
                _ => "circulation.updated"
            };
        }

        private static bool TryGetTransactionItems(JsonElement root, out IEnumerable<JsonElement> items)
        {
            items = Enumerable.Empty<JsonElement>();
            JsonElement itemsElement = default;

            if (root.ValueKind == JsonValueKind.Array)
            {
                items = root.EnumerateArray();
                return true;
            }
            if (root.TryGetProperty("items", out itemsElement) || root.TryGetProperty("Items", out itemsElement))
            {
                if (itemsElement.ValueKind == JsonValueKind.Array)
                {
                    items = itemsElement.EnumerateArray();
                    return true;
                }
            }
            return false;
        }

        private static string? GetJsonString(JsonElement element, params string[] propertyNames)
        {
            foreach (var name in propertyNames)
            {
                if (element.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.String)
                    return value.GetString();
            }

            return null;
        }

        private static DateTime? GetJsonDateTime(JsonElement element, params string[] propertyNames)
        {
            foreach (var name in propertyNames)
            {
                if (!element.TryGetProperty(name, out var value))
                    continue;

                if (value.ValueKind == JsonValueKind.String)
                {
                    var raw = value.GetString();
                    if (!string.IsNullOrWhiteSpace(raw))
                    {
                        // Nếu chuỗi có Z hoặc +07:00
                        if (raw.EndsWith("Z", StringComparison.OrdinalIgnoreCase) || raw.Contains("+") || (raw.Contains("T") && raw.LastIndexOf("-") > raw.IndexOf("T")))
                        {
                            if (DateTimeOffset.TryParse(raw, null, System.Globalization.DateTimeStyles.RoundtripKind, out var dto))
                                return dto.UtcDateTime;
                        }

                        // No timezone info: N2 likely sends Vietnam local time → subtract 7h to get UTC
                        if (DateTime.TryParse(raw, out var parsed))
                            return parsed.AddHours(-7);
                    }
                }
                else if (value.ValueKind == JsonValueKind.Number && value.TryGetInt64(out var unixMs))
                {
                    return DateTimeOffset.FromUnixTimeMilliseconds(unixMs).UtcDateTime;
                }
            }

            return null;
        }
    }
}
