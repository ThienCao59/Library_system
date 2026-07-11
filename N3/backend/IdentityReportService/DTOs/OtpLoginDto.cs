using System.ComponentModel.DataAnnotations;

namespace IdentityReportService.DTOs
{
    /// <summary>Request gửi OTP — chỉ cần CardNumber từ mã QR.</summary>
    public class RequestOtpDto
    {
        [Required]
        public string CardNumber { get; set; } = null!;
    }

    /// <summary>Request đăng nhập bằng OTP sau khi quét QR.</summary>
    public class LoginWithOtpDto
    {
        [Required]
        public string CardNumber { get; set; } = null!;

        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Otp { get; set; } = null!;
    }
}
