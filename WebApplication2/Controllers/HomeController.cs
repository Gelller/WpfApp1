using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Fruits = new string[] { "Яблоко", "Апельсин", "Груша" };
            ViewBag.Cities = new string[] { "Москва", "Лондон", "Париж" };

            string message = "Это HTML-элемент: <input>";

            return View((object)message);
        }

        public ActionResult CreateUser()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult CreateUser(User User)
        {
            return View(User);
        }
    }
}

