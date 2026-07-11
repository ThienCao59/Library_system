using System;

namespace IdentityReportService.Models
{
    public class EventLog
    {
        public Guid Id { get; set; }
        public string EventType { get; set; } = null!;
        public string Data { get; set; } = null!; // JSON string containing event payload
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string SourceService { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
