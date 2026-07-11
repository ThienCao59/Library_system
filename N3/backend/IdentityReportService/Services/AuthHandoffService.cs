using System.Security.Cryptography;
using IdentityReportService.DTOs;
using IdentityReportService.Models;
using Microsoft.Extensions.Caching.Memory;

namespace IdentityReportService.Services
{
    public class AuthHandoffService : IAuthHandoffService
    {
        private const string CacheKeyPrefix = "auth-handoff:";
        private const int ExpirySeconds = 60;

        private readonly IMemoryCache _cache;

        public AuthHandoffService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public HandoffCodeDto CreateHandoffCode(AuthHandoffPayload payload)
        {
            var code = GenerateCode();
            var cacheKey = CacheKeyPrefix + code;

            _cache.Set(cacheKey, payload, TimeSpan.FromSeconds(ExpirySeconds));

            return new HandoffCodeDto
            {
                Code = code,
                ExpiresInSeconds = ExpirySeconds
            };
        }

        public AuthResponseDto? RedeemHandoffCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;

            var cacheKey = CacheKeyPrefix + code.Trim();
            if (!_cache.TryGetValue<AuthHandoffPayload>(cacheKey, out var payload) || payload == null)
                return null;

            _cache.Remove(cacheKey);

            return new AuthResponseDto
            {
                AccessToken = payload.AccessToken,
                RefreshToken = payload.RefreshToken,
                Username = payload.Username,
                Email = payload.Email,
                Role = payload.Role,
                UserId = payload.UserId,
                CardNumber = payload.CardNumber
            };
        }

        private static string GenerateCode()
        {
            var bytes = new byte[32];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }
    }
}
