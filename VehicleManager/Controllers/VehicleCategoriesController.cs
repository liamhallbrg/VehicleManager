using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq; 
using VehicleManager.Models;

namespace VehicleManager.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class VehicleCategoriesController : Controller
    {
        private readonly HttpClient client;

        public VehicleCategoriesController(HttpClient httpClient)
        {
            client = httpClient;
            client.BaseAddress = new Uri("https://localhost:7127/");
            client.DefaultRequestHeaders.Clear();
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? jwtToken = context.HttpContext.Request.Cookies["jwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            base.OnActionExecuting(context);
        }

        // GET: VehicleCategories
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var categories = await client.GetFromJsonAsync<List<VehicleCategory>>("api/vehicleCategories");

            var headers = client.DefaultRequestHeaders;
            foreach (var header in headers)
            {
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine(header.Key + ": " + header.Value.FirstOrDefault());
            }

            if (categories == null)
            {
                return Problem("Entity set 'categories' is null.");
            }

            return View(categories);
        }

        // GET: VehicleCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string filter = $"c => c.VehicleCategoryId == {id}";

            var cars = await client.GetFromJsonAsync<List<Car>>($"api/cars?filter={filter}");
            var vehicleCategory = await client.GetFromJsonAsync<VehicleCategory>($"api/vehicleCategories/{id}");

            if (vehicleCategory == null)
            {
                return NotFound();
            }

            vehicleCategory.Cars = cars;
            return View(vehicleCategory);
        }

        // GET: VehicleCategories/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,PricePerDay")] VehicleCategory vehicleCategory)
        {
            if (ModelState.IsValid)
            {
                var response = await client.PostAsJsonAsync("api/vehicleCategories", vehicleCategory);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ModelState.AddModelError(string.Empty, "Unauthorized");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error, please try again later.");
                }
            }
            return View(vehicleCategory);
        }

        // GET: VehicleCategories/Edit/5v
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleCategory = await client.GetFromJsonAsync<VehicleCategory>($"api/vehicleCategories/{id}");
            if (vehicleCategory == null)
            {
                return NotFound();
            }
            return View(vehicleCategory);
        }

        // POST: VehicleCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,PricePerDay")] VehicleCategory vehicleCategory)
        {
            if (id != vehicleCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await client.PutAsJsonAsync($"api/vehicleCategories/{id}", vehicleCategory);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(string.Empty, "Server error, please try again later.");
            return View(vehicleCategory);
        }

        // GET: VehicleCategories/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleCategory = await client.GetFromJsonAsync<VehicleCategory>($"api/vehicleCategories/{id}");
            if (vehicleCategory == null)
            {
                return NotFound();
            }

            return View(vehicleCategory);
        }

        // POST: VehicleCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await client.GetFromJsonAsync<VehicleCategory>($"api/vehicleCategories/{id}");
            if (category != null)
            {
                var response = await client.DeleteAsync($"api/vehicleCategories/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(string.Empty, "Server error, please try again later.");
            return View(category);
        }

    }
}
