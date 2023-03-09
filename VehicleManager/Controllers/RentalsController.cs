using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using VehicleManager.Data;
using VehicleManager.Helpers;
using VehicleManager.Models;
using VehicleManager.ViewModels;

namespace VehicleManager.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IRepository<Rental> rentalRepo;
        private readonly IRepository<Customer> customerRepo;
        private readonly IRepository<Car> carRepo;
        private readonly IRepository<VehicleCategory> categoryRepo;

        public RentalsController(IRepository<Rental> rentalRepo, IRepository<Customer> customerRepo, IRepository<Car> carRepo, IRepository<VehicleCategory> categoryRepo)
        {

            this.rentalRepo = rentalRepo;
            this.customerRepo = customerRepo;
            this.carRepo = carRepo;
            this.categoryRepo = categoryRepo;
        }

        // GET: Rentals



        public async Task<IActionResult> Index(List<int> readyForPickUp)
        {
            if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }
            if (readyForPickUp != null && readyForPickUp.Any())
            {
                foreach (var rentalId in readyForPickUp)
                {
                    var rental = await rentalRepo.GetByIdAsync(rentalId);
                    if (rental != null)
                    {
                        rental.ReadyForPickUp = true;
                        await rentalRepo.UpdateAsync(rental);
                    }
                }
            }

            var rentals = await rentalRepo.GetAllAsync();
            var rentalViewModels = new List<RentalViewModel>();

            foreach (var rental in rentals)
            {
                var car = await carRepo.GetByIdAsync(rental.CarId);
                var customer = await customerRepo.GetByIdAsync(rental.CustomerId);

                if (car is null || customer is null)
                {
                    return NotFound();
                }
                var rentalViewModel = new RentalViewModel
                {
                    Id = rental.RentalId,
                    PickUpDate = rental.PickUpDate,
                    ReturnDate = rental.ReturnDate,
                    BookingMade = rental.BookingMade,
                    TotalPrice = rental.TotalPrice,
                    PlateNumber = car.PlateNumber,
                    FullName = customer.FullName,
                    CarId = car.CarId,
                    ReadyForPickUp= rental.ReadyForPickUp
                };
                rentalViewModels.Add(rentalViewModel);
            }

            //ViewBag.Rentals = await rentalRepo.GetAllAsync();


            return await rentalRepo.GetAllAsync() != null ?
                        View(rentalViewModels) :
                        Problem("Rental is null.");
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }
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
        public IActionResult Create(int carId, DateTime pickupDate, DateTime returnDate)
        {
            var car = carRepo.GetByIdAsync(carId).Result;
            if (car == null)
            {
                return Redirect("/Home");
            }

            var category = categoryRepo.GetByIdAsync(car.VehicleCategoryId).Result;
            if (category == null)
            {
                return NotFound();
            }

            RentalCustomerVM rentalCustomerVM = new()
            {
                PickUpDate = pickupDate,
                ReturnDate = returnDate,
                TotalPrice = (returnDate - pickupDate).TotalDays * category.PricePerDay,
                ImgUrl = car.ImgUrl,
                Brand = car.Brand,
                Name = category.Name
            };

            return View(rentalCustomerVM);
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalId,CarId,CustomerId,PickUpDate,ReturnDate,BookingMade,TotalPrice")] Rental rental, Customer customer)
        {
            if (ModelState.IsValid)
            {
                await customerRepo.CreateAsync(customer);
                rental.CustomerId = customer.CustomerId;
                await rentalRepo.CreateAsync(rental);
                Car car = await carRepo.GetByIdAsync(rental.CarId) ?? new();

                RentalCustomerVM customerVM = new()
                {
                    Address = customer.Address,
                    BookingMade = rental.BookingMade,
                    DriverLicenceNr = customer.DriverLicenceNr,
                    Brand = car.Brand,
                    CarId = car.CarId,
                    City = customer.City,
                    CustomerId = customer.CustomerId,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PickUpDate = rental.PickUpDate,
                    ReturnDate = rental.ReturnDate,
                    TotalPrice = rental.TotalPrice,
                    ImgUrl = car.ImgUrl,
                    PlateNumber = car.PlateNumber,
                    RentalId = rental.RentalId
                };

                return RedirectToAction(nameof(Confirmation), customerVM);
            }
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }
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
                        if (!Utilities.IsAdmin())
            {
                return Redirect("/");
            }
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

        //GET Confirmation
        public ActionResult Confirmation(RentalCustomerVM customerVM)
        {
            return customerVM != null ?
                        View(customerVM) :
                        Problem("The booking is null.");
        }
    }
}
