
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Mail
{
    public class SmtpMailService : IMailService
    {
        public async Task Send(Message message)
        {
            using (var smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtp.PickupDirectoryLocation = @"c:\maildump";
                var msg = new MailMessage
                {
                    Body = message.Body,
                    Subject = message.Subject,
                    From = new MailAddress(message.From)
                };
                msg.To.Add(message.Subject);
                await smtp.SendMailAsync(msg);
            }
        }
    }
}
