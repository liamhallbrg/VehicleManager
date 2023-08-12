using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleManager.Models;
using VehicleManager.ViewModels;

namespace VehicleManager.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class CarsController : Controller
    {
        private readonly HttpClient client;

        public CarsController(HttpClient httpClient)
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

        //GET: Cars
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var cars = await client.GetFromJsonAsync<List<Car>>("api/cars");

            if (cars == null)
            {
                return Problem("Entity set 'carRep'  is null.");
            }
            
            return View(cars);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var car = await client.GetFromJsonAsync<Car>($"api/cars/{id}");

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.VehicleCategory = new SelectList(await client.GetFromJsonAsync<List<VehicleCategory>>("api/vehicleCategories"), "Id", "Name");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,VehicleCategoryId,Brand,Model,Description,PlateNumber,ImgUrl")] Car car)
        {
            if (ModelState.IsValid)
            {
                var response = await client.PostAsJsonAsync("api/cars", car);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.VehicleCategory = new SelectList(await client.GetFromJsonAsync<List<VehicleCategory>>($"api/vehicleCategories"), "Id", "Name");
            return View(car);
        }

        // GET: Cars/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await client.GetFromJsonAsync<Car>($"api/cars/{id}");

            if (car == null)
            {
                return NotFound();
            }

            ViewBag.VehicleCategory = new SelectList(await client.GetFromJsonAsync<List<VehicleCategory>>($"api/vehicleCategories/"), "Id", "Name");

            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleCategoryId,Brand,Model,Description,PlateNumber,ImgUrl")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await client.PutAsJsonAsync($"api/cars/{id}", car);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(string.Empty, "Server error, please try again later.");
            ViewBag.VehicleCategory = new SelectList(await client.GetFromJsonAsync<List<VehicleCategory>>($"api/vehicleCategories/"), "Id", "Name");
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await client.GetFromJsonAsync<Car>($"api/cars/{id}");
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await client.GetFromJsonAsync<Car>($"api/cars/{id}");
            if (car is null)
            {
                return NotFound();
            }

            var response = await client.DeleteAsync($"api/cars/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(car);
        }

        private bool CarExists(int id)
        {
            return client.GetFromJsonAsync<Car>($"api/cars/{id}") != null;
        }
    }
}
