using ManageRegStds.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;

namespace ManageRegStds.Controllers
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
        public IActionResult GetTime()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetTimeJson()
        {
            var currentTime = DateTime.Now;
            return Json(new { time = currentTime.ToString("HH:mm:ss") });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}