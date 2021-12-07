using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
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
