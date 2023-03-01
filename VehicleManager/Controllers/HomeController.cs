using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VehicleManager.Models;

namespace VehicleManager.Controllers
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

        public IActionResult SetUser()
        {
            if (Request.Cookies.ContainsKey("Role"))
            {
                Response.Cookies.Delete("Role");
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult SetAdmin()
        {
            if (!Request.Cookies.ContainsKey("Role"))
            {
                Response.Cookies.Append("Role", "Admin");
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}