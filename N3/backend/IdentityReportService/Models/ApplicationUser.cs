using System;
using Microsoft.AspNetCore.Identity;

namespace IdentityReportService.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Inherits Id, UserName, Email, PasswordHash from IdentityUser
        
        public string Role { get; set; } = "Reader"; // Admin, Librarian, Reader
        public string FullName { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public string? PreviousRefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsWebAuthnEnabled { get; set; } = false;

        // Navigation property
        public LibraryCard? LibraryCard { get; set; }
    }
}
