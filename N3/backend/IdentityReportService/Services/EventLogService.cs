using System.Text.Json;
using IdentityReportService.Data;
using IdentityReportService.Models;

namespace IdentityReportService.Services
{
    public class EventLogService : IEventLogService
    {
        private readonly AppDbContext _context;

        public EventLogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(string eventType, object data, string sourceService = "IdentityReportService")
        {
            _context.EventLogs.Add(new EventLog
            {
                Id = Guid.NewGuid(),
                EventType = eventType,
                Data = JsonSerializer.Serialize(data),
                SourceService = sourceService,
                Timestamp = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            });

            await _context.SaveChangesAsync();
        }
    }
}
