using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = null!;

        public string Role { get; set; } = "Reader";

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? PhoneNumber { get; set; }
    }
}
