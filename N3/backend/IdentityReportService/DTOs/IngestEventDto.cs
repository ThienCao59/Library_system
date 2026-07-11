using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.DTOs
{
    public class IngestEventDto
    {
        [Required]
        public string EventType { get; set; } = null!;

        [Required]
        public object Data { get; set; } = null!;

        public string? SourceService { get; set; }
    }
}
