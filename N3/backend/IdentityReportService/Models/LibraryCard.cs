using System;

namespace IdentityReportService.Models
{
    public class LibraryCard
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        
        public string CardNumber { get; set; } = null!;
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; } = "Active"; // Active, Locked, Expired
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
