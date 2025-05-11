using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using CookingCourseAPI.Services.Interfaces;

namespace CookingCourseAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendPasswordResetEmail(string email, string resetToken)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
            message.To.Add(MailboxAddress.Parse(email));

            message.Subject = "Password Reset Request";

            var body = $"Click the link below to reset your password:\n\n" +
                       $"http://localhost:5043/sneat-1.0.0/mainhtml/reset-password.html?email={email}&token={resetToken}";
        

            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), false);
                await client.AuthenticateAsync(emailSettings["SmtpUser"], emailSettings["SmtpPassword"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                throw;
            }
        }
    }
}
