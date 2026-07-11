using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using IdentityReportService.Data;
using IdentityReportService.DTOs;
using IdentityReportService.Models;
using IdentityReportService.Services;

namespace IdentityReportService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IEventLogService _eventLogService;

        public UserController(
            UserManager<ApplicationUser> userManager,
            AppDbContext context,
            IEventLogService eventLogService)
        {
            _userManager = userManager;
            _context = context;
            _eventLogService = eventLogService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "Unauthorized access." });

            var user = await _context.Users
                .Include(u => u.LibraryCard)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound(new { Message = "User not found." });

            var profileDto = new UserProfileDto
            {
                Id = user.Id,
                Username = user.UserName!,
                Email = user.Email!,
                FullName = user.FullName,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                IsActive = user.IsActive,
                LibraryCard = user.LibraryCard != null ? new LibraryCardDto
                {
                    CardNumber = user.LibraryCard.CardNumber,
                    IssueDate = user.LibraryCard.IssueDate,
                    ExpiryDate = user.LibraryCard.ExpiryDate,
                    Status = user.LibraryCard.Status
                } : null
            };

            return Ok(profileDto);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "Unauthorized access." });

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            // Check if email is updated and is already taken
            if (!string.Equals(user.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var emailExists = await _userManager.FindByEmailAsync(dto.Email);
                if (emailExists != null)
                    return BadRequest(new { Message = "Email is already taken." });
                
                user.Email = dto.Email;
            }

            user.FullName = dto.FullName;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new { Message = "Failed to update profile.", Errors = result.Errors });

            return Ok(new { Message = "Profile updated successfully!" });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "Unauthorized access." });

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
                return BadRequest(new { Message = "Đổi mật khẩu thất bại.", Errors = result.Errors });

            return Ok(new { Message = "Đổi mật khẩu thành công!" });
        }
        [HttpPut("cards/{cardNumber}/status")]
        public async Task<IActionResult> UpdateCardStatus(string cardNumber, [FromBody] CardStatusUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var card = await _context.LibraryCards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
            if (card == null)
                return NotFound(new { Message = "Library card not found." });

            // Chỉ khóa/mở thẻ — không thay đổi trạng thái tài khoản (vd: chưa đóng phí phạt).
            card.Status = dto.Status;
            card.IsActive = dto.Status == "Active";
            
            // Handle card expiry extension if requested
            if (dto.ExtendMonths.HasValue && dto.ExtendMonths.Value > 0)
            {
                card.ExpiryDate = card.ExpiryDate > DateTime.UtcNow 
                    ? card.ExpiryDate.AddMonths(dto.ExtendMonths.Value) 
                    : DateTime.UtcNow.AddMonths(dto.ExtendMonths.Value);
            }

            card.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var cardOwner = await _context.Users.FindAsync(card.UserId);
            await _eventLogService.LogAsync(
                dto.Status == "Active" ? "card.unlocked" : "card.locked",
                new
                {
                    cardNumber,
                    status = dto.Status,
                    ownerName = cardOwner?.FullName,
                    changedBy = User.Identity?.Name
                });

            return Ok(new { Message = $"Library card {cardNumber} updated successfully to status '{dto.Status}'." });
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpPost("staff")]
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaffDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExists = await _userManager.FindByNameAsync(dto.Username);
            if (userExists != null)
                return BadRequest(new { Message = "Username already exists!" });

            var emailExists = await _userManager.FindByEmailAsync(dto.Email);
            if (emailExists != null)
                return BadRequest(new { Message = "Email already exists!" });

            var user = new ApplicationUser
            {
                UserName = dto.Username,
                Email = dto.Email,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                Role = "Librarian",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(new { Message = "Staff creation failed.", Errors = result.Errors });

            await _eventLogService.LogAsync("staff.created", new
            {
                userId = user.Id,
                username = user.UserName,
                fullName = user.FullName,
                role = user.Role,
                createdBy = User.Identity?.Name
            });

            return Ok(new
            {
                Message = "Librarian account created successfully.",
                UserId = user.Id,
                Username = user.UserName,
                Role = user.Role
            });
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpGet("staff")]
        public async Task<IActionResult> GetStaff()
        {
            var staff = await _context.Users
                .Where(u => u.Role == "Librarian" || u.Role == "Admin")
                .OrderByDescending(u => u.CreatedAt)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    u.Email,
                    u.FullName,
                    u.PhoneNumber,
                    u.Role,
                    u.IsActive,
                    u.CreatedAt
                })
                .ToListAsync();

            return Ok(staff);
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpPut("users/{userId}/role")]
        public async Task<IActionResult> UpdateUserRole(string userId, [FromBody] UpdateUserRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            if (user.Role == "Admin")
                return BadRequest(new { Message = "Cannot change role of an Admin account." });

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user.Id == currentUserId)
                return BadRequest(new { Message = "Cannot change your own role." });

            user.Role = dto.Role;
            user.UpdatedAt = DateTime.UtcNow;

            // Revoke sessions when role changes
            user.RefreshToken = null;
            user.PreviousRefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new { Message = "Failed to update role.", Errors = result.Errors });

            return Ok(new { Message = $"User {user.UserName} role updated to {dto.Role}.", Role = user.Role });
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == currentUserId)
                return BadRequest(new { Message = "Cannot delete your own account." });

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            if (user.Role == "Admin")
                return BadRequest(new { Message = "Cannot delete an Admin account." });

            var username = user.UserName;
            var fullName = user.FullName;
            var role = user.Role;

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest(new { Message = "Failed to delete user.", Errors = result.Errors });

            await _eventLogService.LogAsync("user.deleted", new
            {
                userId,
                username,
                fullName,
                role,
                deletedBy = User.Identity?.Name
            });

            return Ok(new { Message = $"User {username} deleted successfully." });
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpPost("readers/bulk-delete")]
        public async Task<IActionResult> BulkDeleteReaders([FromBody] BulkDeleteUsersDto dto)
        {
            if (!ModelState.IsValid || dto.UserIds.Count == 0)
                return BadRequest(new { Message = "No users selected." });

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var deleted = new List<string>();
            var skipped = new List<string>();

            foreach (var userId in dto.UserIds.Distinct())
            {
                if (userId == currentUserId) { skipped.Add(userId); continue; }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null || user.Role != "Reader") { skipped.Add(userId); continue; }

                var username = user.UserName;
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    deleted.Add(username!);
                    await _eventLogService.LogAsync("user.deleted", new
                    {
                        userId,
                        username,
                        fullName = user.FullName,
                        role = user.Role,
                        deletedBy = User.Identity?.Name,
                        bulk = true
                    });
                }
                else { skipped.Add(userId); }
            }

            return Ok(new
            {
                Message = $"Deleted {deleted.Count} reader(s).",
                Deleted = deleted,
                SkippedCount = skipped.Count
            });
        }

        [Authorize(Policy = "RequireAdmin")]
        [HttpPost("staff/bulk-delete")]
        public async Task<IActionResult> BulkDeleteStaff([FromBody] BulkDeleteUsersDto dto)
        {
            if (!ModelState.IsValid || dto.UserIds.Count == 0)
                return BadRequest(new { Message = "No users selected." });

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var deleted = new List<string>();
            var skipped = new List<string>();

            foreach (var userId in dto.UserIds.Distinct())
            {
                if (userId == currentUserId)
                {
                    skipped.Add(userId);
                    continue;
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null || user.Role == "Admin")
                {
                    skipped.Add(userId);
                    continue;
                }

                var username = user.UserName;
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    deleted.Add(username!);
                    await _eventLogService.LogAsync("user.deleted", new
                    {
                        userId,
                        username,
                        fullName = user.FullName,
                        role = user.Role,
                        deletedBy = User.Identity?.Name,
                        bulk = true
                    });
                }
                else
                {
                    skipped.Add(userId);
                }
            }

            return Ok(new
            {
                Message = $"Deleted {deleted.Count} account(s).",
                Deleted = deleted,
                SkippedCount = skipped.Count
            });
        }

        [Authorize(Policy = "RequireAdminOrLibrarian")]
        [DisableRateLimiting]
        [HttpPost("readers/bulk-import")]
        public async Task<IActionResult> BulkImportReaders([FromBody] BulkImportReadersDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.Readers == null || dto.Readers.Count == 0)
                return BadRequest(new { Message = "Danh sách import trống." });

            if (dto.Readers.Count > 500)
                return BadRequest(new { Message = "Mỗi lần import tối đa 500 độc giả." });

            var result = new BulkImportResultDto();
            var usedUsernames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var usedEmails = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var rng = new Random();

            foreach (var item in dto.Readers)
            {
                var username = item.Username?.Trim();
                var email = item.Email?.Trim();
                var fullName = item.FullName?.Trim();
                var password = item.Password?.Trim();

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email)
                    || string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(password))
                {
                    result.ErrorCount++;
                    result.Errors.Add($"Dòng thiếu thông tin bắt buộc: {username ?? email ?? fullName ?? "(trống)"}");
                    continue;
                }

                if (password.Length < 6)
                {
                    result.ErrorCount++;
                    result.Errors.Add($"{username}: Mật khẩu tối thiểu 6 ký tự");
                    continue;
                }

                if (!usedUsernames.Add(username))
                {
                    result.ErrorCount++;
                    result.Errors.Add($"{username}: Trùng username trong file Excel");
                    continue;
                }

                if (!usedEmails.Add(email))
                {
                    result.ErrorCount++;
                    result.Errors.Add($"{username}: Trùng email trong file Excel");
                    continue;
                }

                if (await _userManager.FindByNameAsync(username) != null)
                {
                    result.ErrorCount++;
                    result.Errors.Add($"{username}: Username đã tồn tại");
                    continue;
                }

                if (await _userManager.FindByEmailAsync(email) != null)
                {
                    result.ErrorCount++;
                    result.Errors.Add($"{username}: Email đã tồn tại");
                    continue;
                }

                var user = new ApplicationUser
                {
                    UserName = username,
                    Email = email,
                    FullName = fullName,
                    PhoneNumber = string.IsNullOrWhiteSpace(item.PhoneNumber) ? null : item.PhoneNumber.Trim(),
                    Role = "Reader",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                var createResult = await _userManager.CreateAsync(user, password);
                if (!createResult.Succeeded)
                {
                    result.ErrorCount++;
                    result.Errors.Add($"{username}: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                    continue;
                }

                var libraryCard = new LibraryCard
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    CardNumber = "LIB-" + rng.Next(100000, 999999),
                    IssueDate = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddYears(1),
                    Status = "Active",
                    CreatedAt = DateTime.UtcNow
                };

                _context.LibraryCards.Add(libraryCard);
                await _context.SaveChangesAsync();

                await _eventLogService.LogAsync("user.registered", new
                {
                    userId = user.Id,
                    username = user.UserName,
                    fullName = user.FullName,
                    role = user.Role,
                    cardNumber = libraryCard.CardNumber,
                    importedBy = User.Identity?.Name,
                    source = "excel-import"
                });

                result.SuccessCount++;
            }

            result.Message = $"Import hoàn tất: {result.SuccessCount} thành công, {result.ErrorCount} thất bại.";
            return Ok(result);
        }

        [Authorize(Policy = "RequireAdminOrLibrarian")]
        [HttpGet("readers")]
        public async Task<IActionResult> GetAllReaders()
        {
            var readers = await _context.Users
                .Include(u => u.LibraryCard)
                .Where(u => u.Role == "Reader")
                .OrderByDescending(u => u.CreatedAt)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    u.Email,
                    u.FullName,
                    u.PhoneNumber,
                    u.Role,
                    u.IsActive,
                    u.CreatedAt,
                    LibraryCard = u.LibraryCard != null ? new
                    {
                        u.LibraryCard.CardNumber,
                        u.LibraryCard.IssueDate,
                        u.LibraryCard.ExpiryDate,
                        u.LibraryCard.Status
                    } : null
                })
                .ToListAsync();

            return Ok(readers);
        }

        [Authorize(Policy = "RequireAdminOrLibrarian")]
        [HttpPut("users/{userId}")]
        public async Task<IActionResult> UpdateUserByAdmin(string userId, [FromBody] UpdateUserByAdminDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            if (!string.Equals(user.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var emailExists = await _userManager.FindByEmailAsync(dto.Email);
                if (emailExists != null)
                    return BadRequest(new { Message = "Email is already taken by another account." });
                
                user.Email = dto.Email;
            }

            user.FullName = dto.FullName;
            if (!string.IsNullOrEmpty(dto.PhoneNumber))
                user.PhoneNumber = dto.PhoneNumber;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new { Message = "Failed to update user.", Errors = result.Errors });

            return Ok(new { Message = $"User {user.UserName} updated successfully!" });
        }

        [Authorize(Policy = "RequireAdminOrLibrarian")]
        [HttpPut("users/{userId}/toggle-status")]
        public async Task<IActionResult> ToggleUserStatus(string userId)
        {
            var user = await _context.Users
                .Include(u => u.LibraryCard)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return NotFound(new { Message = "User not found." });

            user.IsActive = !user.IsActive;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(new { Message = "Failed to update user status.", Errors = result.Errors });

            // Khóa tài khoản → tự khóa thẻ. Mở khóa tài khoản → mở khóa thẻ kèm theo.
            string? cardStatus = null;
            if (user.LibraryCard != null)
            {
                user.LibraryCard.Status = user.IsActive ? "Active" : "Locked";
                user.LibraryCard.IsActive = user.IsActive;
                user.LibraryCard.UpdatedAt = DateTime.UtcNow;
                cardStatus = user.LibraryCard.Status;
                await _context.SaveChangesAsync();
            }

            await _eventLogService.LogAsync(
                user.IsActive ? "user.unlocked" : "user.locked",
                new
                {
                    userId = user.Id,
                    username = user.UserName,
                    fullName = user.FullName,
                    role = user.Role,
                    cardStatus,
                    changedBy = User.Identity?.Name
                });

            var statusLabel = user.IsActive ? "Active" : "Locked";
            var message = user.LibraryCard != null
                ? $"User {user.UserName} is now {statusLabel}. Library card synced to {cardStatus}."
                : $"User {user.UserName} is now {statusLabel}.";

            return Ok(new
            {
                Message = message,
                IsActive = user.IsActive,
                CardStatus = cardStatus
            });
        }

        [Authorize(Policy = "RequireAdminOrLibrarian")]
        [HttpGet("cards")]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _context.LibraryCards
                .Include(c => c.User)
                .Where(c => c.User.Role == "Reader")
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new
                {
                    c.CardNumber,
                    c.IssueDate,
                    c.ExpiryDate,
                    c.Status,
                    c.IsActive,
                    OwnerName = c.User.FullName,
                    OwnerUsername = c.User.UserName
                })
                .ToListAsync();

            return Ok(cards);
        }

        [HttpGet("by-card/{cardNumber}")]
        public async Task<IActionResult> GetUserByCardNumber(string cardNumber)
        {
            var user = await _context.Users
                .Include(u => u.LibraryCard)
                .FirstOrDefaultAsync(u => u.LibraryCard != null && u.LibraryCard.CardNumber == cardNumber);

            if (user == null)
                return NotFound(new { Message = "User with this card number not found." });

            var response = new
            {
                id = user.Id,
                username = user.UserName,
                fullName = user.FullName,
                email = user.Email,
                role = user.Role,
                cardNumber = user.LibraryCard.CardNumber
            };

            return Ok(response);
        }
    }
}
