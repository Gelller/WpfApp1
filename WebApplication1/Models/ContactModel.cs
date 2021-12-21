using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class ContactModel : PageModel
    {
        private readonly IMailService _mailSender;

        public ContactModel(IMailService mailSender)
        {
            _mailSender = mailSender;
        }

        public string Welcome { get; set; }
        [BindProperty]
        public Message Message { get; set; }

        public void OnGet()
        {
            Welcome = "Your contact page.";
        }

        public async Task OnPost()
        {
            await _mailSender.Send(Message);
        }
    }

}
