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
    public class VehicleCategoriesController : Controller
    {
        public IRepository<VehicleCategory> CategoryRepo { get; }

        public VehicleCategoriesController(IRepository<VehicleCategory> CategoryRepo)
        {
            this.CategoryRepo = CategoryRepo;
        }

        // GET: VehicleCategories
        public async Task<IActionResult> Index()
        {
              return await CategoryRepo.GetAllAsync() != null ? 
                          View(await CategoryRepo.GetAllAsync()) :
                          Problem("Vehicle Categories is null.");
        }

        // GET: VehicleCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await CategoryRepo.GetAllAsync() == null)
            {
                return NotFound();
            }

            var vehicleCategory = await CategoryRepo.GetByIdAsync(id);

            if (vehicleCategory == null)
            {
                return NotFound();
            }

            return View(vehicleCategory);
        }

        // GET: VehicleCategories/Create
        public IActionResult Create()
        {
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
                await CategoryRepo.CreateAsync(vehicleCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleCategory);
        }

        // GET: VehicleCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await CategoryRepo.GetAllAsync() == null)
            {
                return NotFound();
            }

            var vehicleCategory = await CategoryRepo.GetByIdAsync(id);
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
                    await CategoryRepo.UpdateAsync(vehicleCategory);
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
            if (id == null || await CategoryRepo.GetAllAsync() == null)
            {
                return NotFound();
            }

            var vehicleCategory = await CategoryRepo.GetByIdAsync(id);
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
            if (await CategoryRepo.GetAllAsync() == null)
            {
                return Problem("Vehicle Categories is null.");
            }
            var vehicleCategory = await CategoryRepo.GetByIdAsync(id);
            if (vehicleCategory != null)
            {
                await CategoryRepo.DeleteAsync(vehicleCategory);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VehicleCategoryExists(int id)
        {
            return CategoryRepo.GetByIdAsync(id) is not null;
        }
    }
}
