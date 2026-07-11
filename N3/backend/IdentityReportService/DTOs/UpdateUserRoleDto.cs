using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.DTOs
{
    public class UpdateUserRoleDto
    {
        [Required(ErrorMessage = "Role is required")]
        [RegularExpression("^(Librarian|Reader)$", ErrorMessage = "Role must be Librarian or Reader")]
        public string Role { get; set; } = null!;
    }
}
