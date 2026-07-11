using System.Text;
using System.Text.Json;
using Fido2NetLib;
using Fido2NetLib.Objects;
using IdentityReportService.Data;
using IdentityReportService.DTOs;
using IdentityReportService.Models;
using IdentityReportService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace IdentityReportService.Controllers
{
    [Route("api/Auth/[controller]")]
    [ApiController]
    public class Fido2Controller : ControllerBase
    {
        private readonly IFido2 _fido2;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly IJwtTokenService _jwtTokenService;

        public Fido2Controller(
            IFido2 fido2,
            UserManager<ApplicationUser> userManager,
            AppDbContext context,
            IMemoryCache cache,
            IJwtTokenService jwtTokenService)
        {
            _fido2 = fido2;
            _userManager = userManager;
            _context = context;
            _cache = cache;
            _jwtTokenService = jwtTokenService;
        }

        // ─── REGISTER OPTIONS ────────────────────────────────────────────────
        [Authorize]
        [HttpPost("registerOptions")]
        public async Task<IActionResult> MakeCredentialOptions([FromBody] JsonElement body)
        {
            var username = body.GetProperty("Username").GetString();
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest(new { Message = "Username required" });

            var user = await _userManager.FindByNameAsync(username);
            if (user == null || !user.IsActive) return Unauthorized();

            var storedDescriptorIds = await _context.FidoStoredCredentials
                .Where(c => c.Username == username)
                .Select(c => c.DescriptorId)
                .ToListAsync();
            var existingCreds = storedDescriptorIds
                .Select(id => new PublicKeyCredentialDescriptor(id))
                .ToList();

            var userHandle = Encoding.UTF8.GetBytes(user.Id);
            var fidoUser = new Fido2User
            {
                DisplayName = user.FullName ?? user.UserName,
                Name = user.UserName,
                Id = userHandle
            };

            var options = _fido2.RequestNewCredential(new RequestNewCredentialParams
                {
                    User = fidoUser,
                    ExcludeCredentials = existingCreds,
                    AuthenticatorSelection = new AuthenticatorSelection(),
                    AttestationPreference = AttestationConveyancePreference.None,
                    Extensions = null
                });

            _cache.Set($"fido2_reg_{username}", options.ToJson(), TimeSpan.FromMinutes(5));
            return Ok(options);
        }

        // ─── REGISTER ────────────────────────────────────────────────────────
        [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> MakeCredential([FromBody] JsonElement body)
        {
            var username = body.GetProperty("Username").GetString();
            var attestationJson = body.GetProperty("AttestationResponse").GetRawText();
            var attestation = JsonSerializer.Deserialize<AuthenticatorAttestationRawResponse>(attestationJson);

            if (string.IsNullOrWhiteSpace(username) || attestation == null)
                return BadRequest(new { Message = "Invalid payload" });

            if (!_cache.TryGetValue($"fido2_reg_{username}", out string? optionsJson))
                return BadRequest(new { Message = "Challenge expired, request new options" });

            var options = CredentialCreateOptions.FromJson(optionsJson);

            IsCredentialIdUniqueToUserAsyncDelegate uniqueCred = async (args, ct) =>
            {
                var creds = await _context.FidoStoredCredentials.Where(c => c.Username == username).ToListAsync(ct);
                var exists = creds.Any(c => c.DescriptorId.SequenceEqual(args.CredentialId) && c.UserHandle.SequenceEqual(args.User.Id));
                return !exists;
            };

            var makeCredParams = new MakeNewCredentialParams
{
    AttestationResponse = attestation,
    OriginalOptions = options,
    IsCredentialIdUniqueToUserCallback = uniqueCred
};

var success = await _fido2.MakeNewCredentialAsync(makeCredParams, System.Threading.CancellationToken.None);

            var newCred = new FidoStoredCredential
            {
                Username = username,
                DescriptorId = success.Id,
                PublicKey = success.PublicKey,
                UserHandle = success.User.Id,
                SignatureCounter = success.SignCount,
                CredType = success.Type.ToString(),
                RegDate = DateTime.UtcNow,
                AaGuid = success.AaGuid
            };
            _context.FidoStoredCredentials.Add(newCred);
            await _context.SaveChangesAsync();
            _cache.Remove($"fido2_reg_{username}");
            return Ok(new { Message = "Đăng ký Face ID / Touch ID thành công!" });
        }

        // ─── LOGIN OPTIONS ────────────────────────────────────────────────────
        [AllowAnonymous]
        [HttpPost("loginOptions")]
        public async Task<IActionResult> MakeAssertionOptions([FromBody] JsonElement body)
        {
            var username = body.GetProperty("Username").GetString();
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest(new { Message = "Username required" });

            var storedDescriptorIds = await _context.FidoStoredCredentials
                .Where(c => c.Username == username)
                .Select(c => c.DescriptorId)
                .ToListAsync();
            var existingCreds = storedDescriptorIds
                .Select(id => new PublicKeyCredentialDescriptor(id))
                .ToList();

            if (!existingCreds.Any())
                return BadRequest(new { Message = "No Face ID credential linked to this account" });

            var options = _fido2.GetAssertionOptions(new GetAssertionOptionsParams
{
    AllowedCredentials = existingCreds,
    UserVerification = UserVerificationRequirement.Preferred,
    Extensions = null
});

            _cache.Set($"fido2_login_{username}", options.ToJson(), TimeSpan.FromMinutes(5));
            return Ok(options);
        }

        // ─── LOGIN ────────────────────────────────────────────────────────────
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> MakeAssertion([FromBody] JsonElement body)
        {
            var username = body.GetProperty("Username").GetString();
            var assertionJson = body.GetProperty("AssertionResponse").GetRawText();
            var assertion = JsonSerializer.Deserialize<AuthenticatorAssertionRawResponse>(assertionJson);

            if (string.IsNullOrWhiteSpace(username) || assertion == null)
                return BadRequest(new { Message = "Invalid payload" });

            if (!_cache.TryGetValue($"fido2_login_{username}", out string? optionsJson))
                return BadRequest(new { Message = "Challenge expired, request new login options" });

            var options = AssertionOptions.FromJson(optionsJson);

            var rawId = assertion.RawId;
            var userCreds = await _context.FidoStoredCredentials
                .Where(c => c.Username == username)
                .ToListAsync();
            var cred = userCreds.FirstOrDefault(c => c.DescriptorId.SequenceEqual(rawId));

            if (cred == null)
                return BadRequest(new { Message = "Invalid credential" });

            Fido2NetLib.IsUserHandleOwnerOfCredentialIdAsync ownerCheck = async (args, ct) =>
            {
                var creds = await _context.FidoStoredCredentials.Where(c => c.Username == username).ToListAsync(ct);
                var exists = creds.Any(c => c.DescriptorId.SequenceEqual(args.CredentialId) && c.UserHandle.SequenceEqual(args.UserHandle));
                return exists;
            };

            var success = await _fido2.MakeAssertionAsync(new MakeAssertionParams
{
    AssertionResponse = assertion,
    OriginalOptions = options,
    StoredPublicKey = cred.PublicKey,
    StoredSignatureCounter = cred.SignatureCounter,
    IsUserHandleOwnerOfCredentialIdCallback = ownerCheck
}, System.Threading.CancellationToken.None);

            cred.SignatureCounter = success.SignCount;
            await _context.SaveChangesAsync();

            var user = await _userManager.FindByNameAsync(username);
            if (user == null || !user.IsActive) return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 0) roles = new List<string> { user.Role };
            var card = await _context.LibraryCards.FirstOrDefaultAsync(c => c.UserId == user.Id);

            var accessToken = _jwtTokenService.GenerateAccessToken(user, roles, card?.CardNumber);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Username = user.UserName!,
                Email = user.Email!,
                Role = user.Role,
                UserId = user.Id,
                CardNumber = card?.CardNumber
            });
        }


    }
}
