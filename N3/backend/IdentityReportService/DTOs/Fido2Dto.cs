using Fido2NetLib;

namespace IdentityReportService.DTOs
{
    public class Fido2LoginDto
    {
        public string Username { get; set; } = string.Empty;
    }

    public class Fido2LoginAssertionDto
    {
        public string Username { get; set; } = string.Empty;
        public AuthenticatorAssertionRawResponse AssertionResponse { get; set; } = null!;
    }
}
