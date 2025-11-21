using System.Net;
using System.Net.Mail;

namespace VentasSystemAPI.Services
{
    public class EmailService(IConfiguration config) : IEmailService
    {
        private readonly IConfiguration _config = config;

        public async Task SendEmailAsync(string toAddress, string subject, string body)
        {
            string fromAddress = _config.GetSection("AppSettings:EMAIL_ADDRESS").Value ?? throw new InvalidOperationException("From address not provided");
            string fromPassword = _config.GetSection("AppSettings:EMAIL_PASS").Value ?? throw new InvalidOperationException("Password not provided");
            string aliasName = _config.GetSection("AppSettings:EMAIL_ALIAS").Value ?? "";
            string smtpHost = _config.GetSection("AppSettings:SMTP_HOST").Value ?? throw new InvalidOperationException("SMTP Host not provided");
            int smtpPort = int.Parse(_config.GetSection("AppSettings:SMTP_PORT").Value ?? "587");

            var credentials = new NetworkCredential(fromAddress, fromPassword);

            var email = new MailMessage()
            {
                From = new MailAddress(fromAddress ?? "", aliasName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            email.To.Add(new MailAddress(toAddress));

            using var smtp = new SmtpClient
            {
                Host = smtpHost,
                Port = smtpPort,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = credentials
            };
            await smtp.SendMailAsync(email);
        }
    }
}
