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

namespace VehicleManager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private readonly HttpClient client;

        public CustomersController(HttpClient httpClient)
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

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await client.GetFromJsonAsync<List<Customer>>("api/customers");

            if (customers == null)
            {
                return Problem("Entity set 'customers' is null.");
            }

            return View(customers);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await client.GetFromJsonAsync<Customer>($"api/customers/{id}");
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Address,City,DriverLicenceNr")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var response = await client.PostAsJsonAsync("api/customers", customer);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(string.Empty, "Server error, please try again later.");
            return View(customer);


        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await client.GetFromJsonAsync<Customer>($"api/customers/{id}");
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To ftect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Address,City,DriverLicenceNr")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await client.PutAsJsonAsync($"api/customers/{id}", customer);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(string.Empty, "Server error, please try again later.");
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await client.GetFromJsonAsync<Customer>($"api/customers/{id}");
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await client.GetFromJsonAsync<Customer>($"api/customers/{id}");
            if (customer != null)
            {
                var response = await client.DeleteAsync($"api/customers/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(string.Empty, "Server error, please try again later.");
            return View(customer);
        }
    }
}
