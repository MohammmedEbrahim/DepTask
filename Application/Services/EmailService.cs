using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Security;
using MimeKit.Text;
using MailKit.Net.Smtp;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task SendEmailAsync(string to , string subject , string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using (var smtp = new SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync(
                        _configuration["EmailSettings:SmtpServer"] ,
                        int.Parse(_configuration["EmailSettings:Port"]) ,
                        SecureSocketOptions.StartTls);

                    await smtp.AuthenticateAsync(
                        _configuration["EmailSettings:Username"] ,
                        _configuration["EmailSettings:Password"]);

                    await smtp.SendAsync(email);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                    throw;
                }
                finally
                {
                    await smtp.DisconnectAsync(true);
                }
            }
        }
    }
}
