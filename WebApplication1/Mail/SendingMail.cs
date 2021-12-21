using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using WebApplication1.Models;

namespace WebApplication1.Mail
{
    public sealed class MessageGateway : IDisposable
    {
        private readonly MailGatewayOptions _options;
        private readonly SmtpClient _client = new SmtpClient();

        public MessageGateway(MailGatewayOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options;

            _client.Connect(options.SMTPServer, options.Port);
            _client.Authenticate(options.Sender, options.Password);
        }

        public async Task SendMessage(Message message)
        {
            MimeMessage emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_options.SenderName, _options.Sender));
            emailMessage.To.Add(new MailboxAddress(message.Body, message.From));
            emailMessage.Subject = message.Subject;
           // emailMessage.Body = new TextPart(message.IsHtml ? MimeKit.Text.TextFormat.Html : MimeKit.Text.TextFormat.Text)
          //  {
          //      Text = message.Body
           // };

            await _client.SendAsync(emailMessage);
        }

        public void Dispose()
        {
            _client.Disconnect(true);
            _client.Dispose();
        }
    }


}
