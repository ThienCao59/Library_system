using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IdentityReportService.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"] ?? "smtp.gmail.com";
            var portStr = _configuration["EmailSettings:Port"] ?? "587";
            var senderEmail = _configuration["EmailSettings:SenderEmail"] ?? "";
            var senderPassword = _configuration["EmailSettings:SenderPassword"] ?? "";

            int port = int.TryParse(portStr, out var p) ? p : 587;

            // If the sender configuration is empty or still placeholder, fallback gracefully
            if (string.IsNullOrEmpty(senderEmail) || 
                senderEmail.Equals("your-gmail@gmail.com", StringComparison.OrdinalIgnoreCase) || 
                string.IsNullOrEmpty(senderPassword))
            {
                _logger.LogWarning("Email settings are not fully configured. Email was NOT sent to {ToEmail}. Subject: {Subject}. Message: {Message}", 
                    toEmail, subject, htmlMessage);
                return;
            }

            try
            {
                using var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(senderEmail, "SmartLib System");
                mailMessage.To.Add(new MailAddress(toEmail));
                mailMessage.Subject = subject;
                mailMessage.Body = htmlMessage;
                mailMessage.IsBodyHtml = true;

                using var smtpClient = new SmtpClient(smtpServer, port);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtpClient.EnableSsl = true;

                _logger.LogInformation("Sending email to {ToEmail} via {SmtpServer}:{Port}...", toEmail, smtpServer, port);
                await smtpClient.SendMailAsync(mailMessage);
                _logger.LogInformation("Email sent successfully to {ToEmail}!", toEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {ToEmail}. Error: {ErrorMessage}", toEmail, ex.Message);
                // Throwing is optional, but for robust API we log it so that a reset can still be checked in local logs
                throw new InvalidOperationException($"Lỗi gửi Email xác nhận: {ex.Message}", ex);
            }
        }
    }
}
