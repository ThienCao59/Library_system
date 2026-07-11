using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.DTOs
{
    public class RedeemHandoffDto
    {
        [Required]
        public string Code { get; set; } = null!;
    }
}
