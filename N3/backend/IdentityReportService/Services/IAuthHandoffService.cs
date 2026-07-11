using IdentityReportService.DTOs;
using IdentityReportService.Models;

namespace IdentityReportService.Services
{
    public interface IAuthHandoffService
    {
        HandoffCodeDto CreateHandoffCode(AuthHandoffPayload payload);
        AuthResponseDto? RedeemHandoffCode(string code);
    }
}
