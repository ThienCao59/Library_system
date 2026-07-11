using System;

namespace IdentityReportService.DTOs
{
    public class NotificationItemDto
    {
        public Guid Id { get; set; }
        public string EventType { get; set; } = null!;
        public string Data { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public string SourceService { get; set; } = null!;
    }
}
