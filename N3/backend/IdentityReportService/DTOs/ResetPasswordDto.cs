using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.DTOs
{
    public class ResetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; } = null!;

        [Required]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu nhập lại không khớp!")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
