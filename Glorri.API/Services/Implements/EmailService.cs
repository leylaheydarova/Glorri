using Glorri.API.Models;
using Glorri.API.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;


namespace Glorri.API.Services.Implements
{
    public class EmailService : IEmailService
    {
        readonly EmailSetting _setting;

        public EmailService(IOptions<EmailSetting> setting)
        {
            _setting = setting.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_setting.Name, _setting.From));
                message.To.Add(MailboxAddress.Parse(to));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_setting.SmtpServer, _setting.Port, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_setting.Username, _setting.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch
            {
                throw new InvalidOperationException("Send message failed");
            }
        }
    }
}
