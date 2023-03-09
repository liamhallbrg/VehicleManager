using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleManager.Data;
using VehicleManager.Helpers;
using VehicleManager.Models;

namespace VehicleManager.Controllers
{
    public class VehicleCategoriesController : Controller
    {
        private readonly IRepository<VehicleCategory> categoryRepo;
        private readonly IRepository<Car> carRepo;

        public VehicleCategoriesController(IRepository<VehicleCategory> categoryRepo, IRepository<Car> carRepo)
        {
            this.categoryRepo = categoryRepo;
            this.carRepo = carRepo;
        }

        // GET: VehicleCategories
        public async Task<IActionResult> Index()
        {
            if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }
            return await categoryRepo.GetAllAsync() != null ? 
                          View(await categoryRepo.GetAllAsync()) :
                          Problem("Vehicle Categories is null.");
        }

        // GET: VehicleCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }
            if (id == null || await categoryRepo.GetAllAsync() == null)
            {
                return NotFound();
            }
            var cars = await carRepo.GetAllAsync(c => c.VehicleCategoryId == id);
            var vehicleCategory = await categoryRepo.GetByIdAsync(id);

            if (vehicleCategory == null)
            {
                return NotFound();
            }

            vehicleCategory.Cars = cars;
            return View(vehicleCategory);
        }

        // GET: VehicleCategories/Create
        public IActionResult Create()
        {
            if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }
            return View();
        }

        // POST: VehicleCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleCategoryId,Name,Description,PricePerDay")] VehicleCategory vehicleCategory)
        {
            if (ModelState.IsValid)
            {
                await categoryRepo.CreateAsync(vehicleCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleCategory);
        }

        // GET: VehicleCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }
            if (id == null || await categoryRepo.GetAllAsync() == null)
            {
                return NotFound();
            }

            var vehicleCategory = await categoryRepo.GetByIdAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("VehicleCategoryId,Name,Description,PricePerDay")] VehicleCategory vehicleCategory)
        {
            if (id != vehicleCategory.VehicleCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await categoryRepo.UpdateAsync(vehicleCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleCategoryExists(vehicleCategory.VehicleCategoryId))
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
            return View(vehicleCategory);
        }

        // GET: VehicleCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }
            if (id == null || await categoryRepo.GetAllAsync() == null)
            {
                return NotFound();
            }

            var vehicleCategory = await categoryRepo.GetByIdAsync(id);
            if (vehicleCategory == null)
            {
                return NotFound();
            }

            return View(vehicleCategory);
        }

        // POST: VehicleCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await categoryRepo.GetAllAsync() == null)
            {
                return Problem("Vehicle Categories is null.");
            }
            var vehicleCategory = await categoryRepo.GetByIdAsync(id);
            if (vehicleCategory != null)
            {
                await categoryRepo.DeleteAsync(vehicleCategory);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VehicleCategoryExists(int id)
        {
            return categoryRepo.GetByIdAsync(id) is not null;
        }
    }
}
