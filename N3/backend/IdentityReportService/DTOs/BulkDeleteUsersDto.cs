using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.DTOs
{
    public class BulkDeleteUsersDto
    {
        [Required]
        public List<string> UserIds { get; set; } = new();
    }
}
