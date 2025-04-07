using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Aplication.Interfaces;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailCodeAsync(string email, string code)
        {
            try
            {
                var emailSettings = _configuration.GetSection("EmailSettings");
                var senderEmail = emailSettings["SenderEmail"];
                var senderName = emailSettings["SenderName"];
                var smtpServer = emailSettings["SmtpServer"];
                var port = int.Parse(emailSettings["Port"]);
                var password = emailSettings["Password"];

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(senderName, senderEmail));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Código de Confirmación";

                message.Body = new TextPart("html")
                {
                    Text = $@"
                        <html>
                        <body style='font-family: Arial, sans-serif; background-color: #F7F1EC; padding: 20px; color: #363433;'>
                            <div style='max-width: 600px; margin: auto; background-color: #ffffff; border-left: 5px solid #D3624B; padding: 20px; border-radius: 8px;'>
                                <h2 style='color: #D3624B;'>Código de Confirmación</h2>
                                <p style='color: #928E8D;'>Hola,</p>
                                <p style='font-size: 16px;'>Tu código de confirmación es:</p>
                                <p style='font-size: 24px; font-weight: bold; color: #679693; text-align: center;'>{code}</p>
                                <hr style='border: none; border-top: 1px solid #D9D9D9; margin: 20px 0;' />
                                <p style='font-size: 12px; color: #928E8D;'>Si no solicitaste este código, podés ignorar este mensaje.</p>
                            </div>
                        </body>
                        </html>"
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpServer, port, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(senderEmail, password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando correo: {ex.Message}");
                return false;
            }
        }
    }
}
