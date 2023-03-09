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
using VehicleManager.ViewModels;

namespace VehicleManager.Controllers
{
    public class CarsController : Controller
    {
        private readonly IRepository<Car> carRep;
        private readonly IRepository<VehicleCategory> categoryRep;

        public CarsController(IRepository<Car> carRep, IRepository<VehicleCategory> categoryRep)
        {
            this.carRep = carRep;
            this.categoryRep = categoryRep;
        }

        //GET: Cars
        public async Task<IActionResult> Index()
        {
            if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }

            if (carRep == null)
            {
                return Problem("Entity set 'carRep'  is null.");
            }  
            return View(await carRep.GetAllAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }

            if (id == null || carRep == null)
            {
                return NotFound();
            }

            var car = await carRep.GetByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public async Task<IActionResult> Create()
        {
            if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }
            ViewBag.VehicleCategory = new SelectList(await categoryRep.GetAllAsync(), "VehicleCategoryId", "Name");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,VehicleCategoryId,Brand,Model,Description,PlateNumber,ImgUrl")] Car car)
        {
            if (ModelState.IsValid)
            {
                await carRep.CreateAsync(car);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.VehicleCategory = new SelectList(await categoryRep.GetAllAsync(), "VehicleCategoryId", "Name");
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }
            if (id == null || carRep == null)
            {
                return NotFound();
            }

            var car = await carRep.GetByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            ViewBag.VehicleCategory = new SelectList(await categoryRep.GetAllAsync(), "VehicleCategoryId", "Name");

            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,VehicleCategoryId,Brand,Model,Description,PlateNumber,ImgUrl")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await carRep.UpdateAsync(car);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
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
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }
            if (id == null || carRep == null)
            {
                return NotFound();
            }

            var car = await carRep.GetByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (carRep == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
            }
            var car = await carRep.GetByIdAsync(id);
            if (car != null)
            {
                await carRep.DeleteAsync(car);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return carRep.GetByIdAsync(id) != null;
        }
    }
}
