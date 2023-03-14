using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;
using VehicleManager.Data;
using VehicleManager.Helpers;
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
            IndexVM indexVM = new()
            {
                VehicleCategories = await categoryRepository.GetAllAsync()
            };
            foreach (var category in indexVM.VehicleCategories)
            {
                category.Cars = await carRepository.GetAllAsync(s => s.VehicleCategoryId == category.VehicleCategoryId);
            }
            return View(indexVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int vehicleCategoryId, DateTime pickupDate, DateTime returnDate)
        {

            var category = await categoryRepository.GetByIdAsync(vehicleCategoryId);
                if (category == null) return Redirect("/"); //TODO: Felhantering om kategori ID inte finns
            var allRentals = await rentalRepository.GetAllAsync();
            Index2VM index2VM = new();
            Expression<Func<Car, bool>> filter = s => s.VehicleCategoryId == vehicleCategoryId;

            index2VM.Category = category;
            index2VM.Selected = vehicleCategoryId;
            index2VM.VehicleCategories = await categoryRepository.GetAllAsync();
            index2VM.Cars = await carRepository.GetAllAsync(filter);
            index2VM.PickupDate = pickupDate;
            index2VM.ReturnDate = returnDate;
            index2VM.TotalPrice = (returnDate - pickupDate).TotalDays * category.PricePerDay;

            foreach (var car in index2VM.Cars.ToList())
            {
                if(allRentals.Where(r => r.CarId == car.CarId && (pickupDate < r.ReturnDate && r.PickUpDate < returnDate)).Any())
                {
                    index2VM.Cars.Remove(car);
                }
            }
            return View("Index2", index2VM);
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