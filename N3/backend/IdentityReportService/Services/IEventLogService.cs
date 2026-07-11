namespace IdentityReportService.Services
{
    public interface IEventLogService
    {
        Task LogAsync(string eventType, object data, string sourceService = "IdentityReportService");
    }
}
