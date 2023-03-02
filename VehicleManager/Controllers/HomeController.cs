using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VehicleManager.Data;
using VehicleManager.Models;
using VehicleManager.ViewModels;

namespace VehicleManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Car> carRepository;
        private readonly IRepository<VehicleCategory> categoryRepository;
        private readonly IRepository<Rental> rentalRepository;

        public HomeController(ILogger<HomeController> logger, IRepository<Car> carRepository, IRepository<VehicleCategory> categoryRepository, IRepository<Rental> rentalRepository)
        {
            _logger = logger;
            this.carRepository = carRepository;
            this.categoryRepository = categoryRepository;
            this.rentalRepository = rentalRepository;
        }

        public async Task<IActionResult> Index()
        {
            IndexVM indexVM = new();
            indexVM.VehicleCategories = await categoryRepository.GetAllAsync();
            return View(indexVM);
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