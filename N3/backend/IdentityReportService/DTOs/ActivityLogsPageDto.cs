using System.Collections.Generic;

namespace IdentityReportService.DTOs
{
    public class ActivityLogsPageDto
    {
        public List<NotificationItemDto> Items { get; set; } = new();
        public int Total { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
