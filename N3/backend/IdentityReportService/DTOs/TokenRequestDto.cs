using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.DTOs
{
    public class TokenRequestDto
    {
        [Required(ErrorMessage = "AccessToken is required")]
        public string AccessToken { get; set; } = null!;

        [Required(ErrorMessage = "RefreshToken is required")]
        public string RefreshToken { get; set; } = null!;
    }
}
