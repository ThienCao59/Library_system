using System.Collections.Generic;
using System.Security.Claims;
using IdentityReportService.Models;

namespace IdentityReportService.Services
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(ApplicationUser user, IList<string> roles, string? cardNumber = null);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
