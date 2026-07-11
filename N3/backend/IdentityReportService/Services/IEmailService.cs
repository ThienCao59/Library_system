using System.Threading.Tasks;

namespace IdentityReportService.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
    }
}
