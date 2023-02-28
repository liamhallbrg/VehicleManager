using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleManager.Data;
using VehicleManager.Models;

namespace VehicleManager.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IRepository<Customer> customerRepo;

        public CustomersController(IRepository<Customer> customerRepo)
        {
            this.customerRepo = customerRepo;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
              return await customerRepo.GetAllAsync() != null ? 
                          View(await customerRepo.GetAllAsync()) :
                          Problem("Entity set Customer is null.");
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await customerRepo.GetAllAsync() == null)
            {
                return NotFound();
            }

            var customer = await customerRepo.GetByIdAsync(id);
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
        public async Task<IActionResult> Create([Bind("CustomerId,FirstName,LastName,Email,Address,City,DriverLicenceNr")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await customerRepo.CreateAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await customerRepo.GetAllAsync() == null)
            {
                return NotFound();
            }

            var customer = await customerRepo.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FirstName,LastName,Email,Address,City,DriverLicenceNr")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await customerRepo.UpdateAsync(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await customerRepo.GetAllAsync() == null)
            {
                return NotFound();
            }

            var customer = await customerRepo.GetByIdAsync(id);
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
            if (await customerRepo.GetAllAsync() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Customer'  is null.");
            }
            var customer = await customerRepo.GetByIdAsync(id);
            if (customer != null)
            {
                await customerRepo.DeleteAsync(customer);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return customerRepo.GetByIdAsync(id) is not null;
        }
    }
}
