using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;


namespace WebApplication1.Mail
{
    public sealed class MailGatewayOptions
    {
        private const int DefaultPort = 25;

        public MailGatewayOptions()
        {
            Port = DefaultPort;
        }

        public string SenderName { get; set; }

        public string SMTPServer { get; set; }

        public int Port { get; set; }

        public string Sender { get; set; }

        public string Password { get; set; }
    }

}
