namespace IdentityReportService.Services
{
    public interface IOtpService
    {
        /// <summary>Tạo và lưu OTP 6 số cho userId, trả về mã OTP vừa tạo.</summary>
        string GenerateOtp(string userId);

        /// <summary>Xác thực OTP. Trả true nếu đúng và còn hạn (5 phút), đồng thời xoá OTP khỏi bộ nhớ.</summary>
        bool ValidateOtp(string userId, string otp);
    }
}
