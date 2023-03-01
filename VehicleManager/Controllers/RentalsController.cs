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
    public class RentalsController : Controller
    {
		private readonly IRepository<Rental> rentalRepo;

		public RentalsController(IRepository<Rental> rentalRepo)
        {
           
			this.rentalRepo = rentalRepo;
		}

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
              return await rentalRepo.GetAllAsync() != null ? 
                          View(await rentalRepo.GetAllAsync()) :
                          Problem("Rental is null.");
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await rentalRepo.GetAllAsync() == null)
            {
                return NotFound();
            }

            var rental = await rentalRepo.GetByIdAsync(id);
           
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalId,CarId,CustomerId,PickUpDate,ReturnDate,BookingMade,TotalPrice")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                await rentalRepo.CreateAsync(rental);
                return RedirectToAction(nameof(Index));
            }
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await rentalRepo.GetAllAsync() == null)
            {
                return NotFound();
            }

            var rental = await rentalRepo.GetByIdAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalId,CarId,CustomerId,PickUpDate,ReturnDate,BookingMade,TotalPrice")] Rental rental)
        {
            if (id != rental.RentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await rentalRepo.UpdateAsync(rental);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.RentalId))
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
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await rentalRepo.GetAllAsync() == null)
            {
                return NotFound();
            }

            var rental = await rentalRepo.GetByIdAsync(id);

            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await rentalRepo.GetAllAsync() == null)
            {
                return Problem("Rental is null.");
            }
            var rental = await rentalRepo.GetByIdAsync(id);
            if (rental != null)
            {
                await rentalRepo.DeleteAsync(rental);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
          return rentalRepo.GetByIdAsync(id) is not null;
        }
    }
}
