using Microsoft.Extensions.Configuration;
using Restaurants.Domain.Repositories;
using System.Net;
using System.Net.Mail;

namespace Restaurants.Infrastructure.Repositories
{
    public class EmailRepository(IConfiguration configuration) : IEmailRepository
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient(_configuration["EmailSettings:SMTPHost"])
                {
                    Port = int.Parse(_configuration["EmailSettings:SMTPPort"]),
                    Credentials = new NetworkCredential(
                        _configuration["EmailSettings:SenderEmail"],
                        _configuration["EmailSettings:SenderPassword"]
                    ),
                    EnableSsl = bool.Parse(_configuration["EmailSettings:EnableSSL"])
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:SenderEmail"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email error: {ex.Message}");
                return false;
            }
        }
    }
}
