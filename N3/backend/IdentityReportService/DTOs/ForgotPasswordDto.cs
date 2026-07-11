using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.DTOs
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
