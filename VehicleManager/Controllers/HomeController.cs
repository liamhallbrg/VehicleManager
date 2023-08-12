using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Web;
using VehicleManager.Models;
using VehicleManager.ViewModels;

namespace VehicleManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client;
        private readonly RoleManager<IdentityRole> role;
        private readonly UserManager<IdentityUser> user;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, RoleManager<IdentityRole> role,UserManager<IdentityUser> user)
        {
            _logger = logger;
            client = httpClient;
            client.BaseAddress = new Uri("https://localhost:7127/");
            client.DefaultRequestHeaders.Clear();
            this.role = role;
            this.user = user;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? jwtToken = context.HttpContext.Request.Cookies["jwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            base.OnActionExecuting(context);
        }

        public async Task<IActionResult> Index()
        {
            var categories = await client.GetFromJsonAsync<List<VehicleCategory>>("api/vehicleCategories");
            if (categories is null)
            {
                return NotFound();
            }

            IndexVM indexVM = new()
            {
                VehicleCategories = categories
            };
            foreach (var category in indexVM.VehicleCategories)
            {
                string filter = $"f => f.VehicleCategoryId == {category.Id}";
                category.Cars = await client.GetFromJsonAsync<List<Car>>($"api/cars?filter={filter}");
            }
            return View(indexVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int vehicleCategoryId, DateTime pickupDate, DateTime returnDate)
        {

            var category = await client.GetFromJsonAsync<VehicleCategory>($"api/vehicleCategories/{vehicleCategoryId}");
                if (category == null) return Redirect("/"); //TODO: Felhantering om kategori ID inte finns
            var allRentals = await client.GetFromJsonAsync<List<Rental>>($"api/rentals");
            Index2VM index2VM = new();
            var filter = $"f => f.VehicleCategoryId == {vehicleCategoryId}";

            index2VM.Category = category;
            index2VM.Selected = vehicleCategoryId;
            index2VM.VehicleCategories = await client.GetFromJsonAsync<List<VehicleCategory>>($"api/vehicleCategories");
            index2VM.Cars = await client.GetFromJsonAsync<List<Car>>($"api/cars?filter={filter}");
            index2VM.PickupDate = pickupDate;
            index2VM.ReturnDate = returnDate;
            index2VM.TotalPrice = (returnDate - pickupDate).TotalDays * category.PricePerDay;

            foreach (var car in index2VM.Cars.ToList())
            {
                if(allRentals.Where(r => r.CarId == car.Id && (pickupDate < r.ReturnDate && r.PickUpDate < returnDate)).Any())
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
    }
}