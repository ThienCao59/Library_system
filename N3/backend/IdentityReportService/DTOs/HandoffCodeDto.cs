namespace IdentityReportService.DTOs
{
    public class HandoffCodeDto
    {
        public string Code { get; set; } = null!;
        public int ExpiresInSeconds { get; set; }
    }
}
