using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.DTOs
{
    public class BulkImportReaderItemDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;

        public string? PhoneNumber { get; set; }
    }

    public class BulkImportReadersDto
    {
        [Required]
        [MaxLength(500)]
        public List<BulkImportReaderItemDto> Readers { get; set; } = new();
    }

    public class BulkImportResultDto
    {
        public int SuccessCount { get; set; }
        public int ErrorCount { get; set; }
        public List<string> Errors { get; set; } = new();
        public string Message { get; set; } = null!;
    }
}
