using System;

namespace IdentityReportService.DTOs
{
    public class UserProfileDto
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        public LibraryCardDto? LibraryCard { get; set; }
    }

    public class LibraryCardDto
    {
        public string CardNumber { get; set; } = null!;
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; } = null!; // Active, Locked, Expired
    }
}
