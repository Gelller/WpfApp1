using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using WebApplication1.Mail;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller    
    {

        private readonly IMailService _mailSender;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IMailService mailSender)
        {
            _logger = logger;
            _mailSender = mailSender;
        }


        public IActionResult Index()
        {      
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task OnPost()
        {
            using (var smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtp.PickupDirectoryLocation = @"c:\maildump";
                var message = new MailMessage
                {
                 //   Body = Message,
                 //   Subject = Message.Subject,
                  //  From = new MailAddress(Message.From)
                };
               // message.To.Add(Message.To);
                await smtp.SendMailAsync(message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult OfficeModel()
        {

            var NewOfficeModel = new List<OfficeModel>();

            NewOfficeModel.Add(new OfficeModel { Id = Guid.NewGuid(), NameOffice = "Office1", NumberEmployees = 10 });
            NewOfficeModel.Add(new OfficeModel { Id = Guid.NewGuid(), NameOffice = "Office2", NumberEmployees = 100 });
            return View(NewOfficeModel);
        }
    }

}
