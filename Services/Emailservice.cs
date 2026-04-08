using MailKit.Net.Smtp; // Ensure this is the only SmtpClient used
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace core8_vue_mysql.Services
{
    public interface IEmailService
    {
        Task sendMail(string to, string fullname, string subject, string msgBody);
        Task sendMailToken(string to, string subject, string msgBody);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // 1. Added 'async' keyword and changed return type to Task
        public async Task sendMail(string to, string fullname, string subject, string msgBody)
        {
            var message = new MimeMessage();
            // 2. Map the "From" address correctly from configuration keys, not a literal email string
            message.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:fromEmail"]));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = msgBody };

            using var smtp = new SmtpClient();
            try
            {
                // 3. Use the configuration value, not the literal string name
                await smtp.ConnectAsync(_configuration["EmailSettings:smtpserver"], 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_configuration["EmailSettings:fromEmail"], _configuration["EmailSettings:emailPassword"]);
                await smtp.SendAsync(message);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }

        public async Task sendMailToken(string to, string subject, string msgBody)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:fromEmail"]));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = msgBody };

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_configuration["EmailSettings:smtpserver"], 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_configuration["EmailSettings:fromEmail"], _configuration["EmailSettings:emailPassword"]);
                await smtp.SendAsync(message);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
