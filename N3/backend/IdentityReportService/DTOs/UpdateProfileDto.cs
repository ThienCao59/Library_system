using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.DTOs
{
    public class UpdateProfileDto
    {
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;
    }
}
