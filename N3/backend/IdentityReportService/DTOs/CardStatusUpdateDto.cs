using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.DTOs
{
    public class CardStatusUpdateDto
    {
        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("^(Active|Locked|Expired)$", ErrorMessage = "Status must be Active, Locked, or Expired")]
        public string Status { get; set; } = null!;
        
        public int? ExtendMonths { get; set; } // Optional: to extend card expiry
    }
}
