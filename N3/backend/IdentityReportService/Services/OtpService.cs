using System.Collections.Concurrent;

namespace IdentityReportService.Services
{
    /// <summary>
    /// Lưu OTP trong bộ nhớ (in-memory) — không cần migration DB.
    /// OTP hết hạn sau 5 phút, chỉ dùng được đúng 1 lần.
    /// </summary>
    public class OtpService : IOtpService
    {
        private record OtpEntry(string Code, DateTime ExpiresAt);

        // Key = userId, Value = OTP entry
        private readonly ConcurrentDictionary<string, OtpEntry> _store = new();

        private static readonly Random _rng = new();

        public string GenerateOtp(string userId)
        {
            // Sinh mã 6 số ngẫu nhiên
            var code = _rng.Next(100000, 999999).ToString();
            var entry = new OtpEntry(code, DateTime.UtcNow.AddMinutes(5));

            // Ghi đè OTP cũ nếu user yêu cầu gửi lại
            _store[userId] = entry;

            return code;
        }

        public bool ValidateOtp(string userId, string otp)
        {
            if (!_store.TryGetValue(userId, out var entry))
                return false;

            if (DateTime.UtcNow > entry.ExpiresAt)
            {
                _store.TryRemove(userId, out _);
                return false;
            }

            if (!string.Equals(entry.Code, otp.Trim(), StringComparison.Ordinal))
                return false;

            // Xoá sau khi dùng — tránh replay attack
            _store.TryRemove(userId, out _);
            return true;
        }
    }
}
