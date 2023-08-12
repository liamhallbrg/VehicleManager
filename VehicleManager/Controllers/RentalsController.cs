using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using VehicleManager.Models;
using VehicleManager.ViewModels;

namespace VehicleManager.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class RentalsController : Controller
    {
        private readonly HttpClient client;
        private readonly IMapper mapper;

        public RentalsController(HttpClient httpClient,IMapper mapper)
        {
            client = httpClient;
            client.BaseAddress = new Uri("https://localhost:7127/");
            client.DefaultRequestHeaders.Clear();
            this.mapper = mapper;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? jwtToken = context.HttpContext.Request.Cookies["jwtToken"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            base.OnActionExecuting(context);
        }

        // GET: Rentals
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(List<int> readyForPickUp)
        {
            if (readyForPickUp != null && readyForPickUp.Any())
            {
                foreach (var rentalId in readyForPickUp)
                {
                    var rental = await client.GetFromJsonAsync<Rental>($"api/rentals/{rentalId}");
                    if (rental != null)
                    {
                        rental.ReadyForPickUp = true;
                        var response = await client.PutAsJsonAsync($"api/rentals/{rentalId}", rental);
                    }
                }
            }

            var rentals = await client.GetFromJsonAsync<List<Rental>>("api/rentals");
            var rentalViewModels = new List<RentalViewModel>();

            foreach (var rental in rentals)
            {
                var car = await client.GetFromJsonAsync<Car>($"api/cars/{rental.CarId}");
                var customer = await client.GetFromJsonAsync<Customer>($"api/customers/{rental.CustomerId}");

                if (car is null || customer is null)
                {
                    return NotFound();
                }
                var rentalViewModel = new RentalViewModel
                {
                    Id = rental.Id,
                    PickUpDate = rental.PickUpDate,
                    ReturnDate = rental.ReturnDate,
                    BookingMade = rental.BookingMade,
                    TotalPrice = rental.TotalPrice,
                    PlateNumber = car.PlateNumber,
                    FullName = customer.FullName,
                    CarId = car.Id,
                    ReadyForPickUp= rental.ReadyForPickUp
                };
                rentalViewModels.Add(rentalViewModel);
            }

            return await client.GetFromJsonAsync<List<Rental>>("api/rentals") != null ?
                        View(rentalViewModels) :
                        Problem("Rental is null.");
        }

        // GET: Rentals/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await client.GetFromJsonAsync<Rental>($"api/rentals/{id}");

            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public async Task<IActionResult> Create(int carId, DateTime pickupDate, DateTime returnDate)
        {
            var car = await client.GetFromJsonAsync<Car>($"api/cars/{carId}");
            if (car == null)
            {
                return Redirect("/Home");
            }

            var category = await client.GetFromJsonAsync<VehicleCategory>($"api/vehicleCategories/{car.VehicleCategoryId}");
            
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
                Model = car.Model,
                Name = category.Name
            };

            return View(rentalCustomerVM);
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,CustomerId,PickUpDate,ReturnDate,BookingMade,TotalPrice")] Rental rental, Customer customer)
        {
            var car = await client.GetFromJsonAsync<Car>($"api/cars/{rental.CarId}") ?? new();

            RentalCustomerVM customerVM = mapper.Map<RentalCustomerVM>(car);
            customerVM = mapper.Map(rental,customerVM);
            customerVM = mapper.Map(customer,customerVM);

            //RentalCustomerVM customerVM = new()
            //{
            //    Address = customer.Address,
            //    BookingMade = rental.BookingMade,
            //    DriverLicenceNr = customer.DriverLicenceNr,
            //    Brand = car.Brand,
            //    CarId = car.Id,
            //    City = customer.City,
            //    Email = customer.Email,
            //    FirstName = customer.FirstName,
            //    LastName = customer.LastName,
            //    PickUpDate = rental.PickUpDate,
            //    ReturnDate = rental.ReturnDate,
            //    TotalPrice = rental.TotalPrice,
            //    ImgUrl = car.ImgUrl,
            //    PlateNumber = car.PlateNumber,
            //};

            if (ModelState.IsValid)
            {
                var customerResponse = await client.PostAsJsonAsync("api/customers", customer);
                var customerString = await customerResponse.Content.ReadAsStringAsync();
                var customerDeserialized = JsonConvert.DeserializeObject<Customer>(customerString);

                if (customerDeserialized is null)
                { return NotFound(); }

                rental.CustomerId = customerDeserialized.Id;
                customerVM.CustomerId = customerDeserialized.Id;

                var rentalResponse = await client.PostAsJsonAsync("api/rentals", rental);
                var rentalString = await rentalResponse.Content.ReadAsStringAsync();
                var rentalDeserialized = JsonConvert.DeserializeObject<Rental>(rentalString);

                if (rentalDeserialized is null)
                { return NotFound(); }

                customerVM.RentalId = rentalDeserialized.Id;

                if (rentalResponse.IsSuccessStatusCode && customerResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Confirmation), customerVM);
                }
            }
            ModelState.AddModelError(string.Empty, "Server error, please try again later.");
            return View(customerVM);
        }

        // GET: Rentals/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await client.GetFromJsonAsync<Rental>($"api/rentals/{id}");
            if (rental == null)
            {
                return NotFound();
            }

            var car = await client.GetFromJsonAsync<Car>($"api/cars/{rental.CarId}");
            if (car == null)
            {
                return Redirect("/Home");
            }

            var category = await client.GetFromJsonAsync<VehicleCategory>($"api/vehicleCategories/{car.VehicleCategoryId}");
            if (category == null)
            {
                return Redirect("/Home");
            }

            rental.TotalPrice = (rental.ReturnDate - rental.PickUpDate).TotalDays * category.PricePerDay;

            ViewBag.Cars = new SelectList(await client.GetFromJsonAsync<List<Car>>("api/cars"), "Id", "PlateNumber");
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("RentalId,CarId,CustomerId,PickUpDate,ReturnDate,BookingMade,TotalPrice")] Rental rental)
        {
            if (id != rental.Id)
            {
                return NotFound();
            }
            Expression<Func<Car, bool>> filter = r => r.Id == rental.Id;

            var thisCarsRentals = await client.GetFromJsonAsync<List<Rental>>($"api/rentals/{filter}");
            var currentRental = thisCarsRentals.Where(r => r.Id == rental.Id).FirstOrDefault();
            if (currentRental == null) 
            {
                return NotFound();
            }
            thisCarsRentals.Remove(currentRental);

            if (thisCarsRentals.Where(r => r.ReturnDate > rental.PickUpDate && r.PickUpDate < rental.ReturnDate).Any())
            {
                ModelState.AddModelError(nameof(rental.PickUpDate), "Already booked during this time period!");
            }

            else if (ModelState.IsValid)
            {
                var response = await client.PutAsJsonAsync($"api/rentals/{id}", rental);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Cars = new SelectList(await client.GetFromJsonAsync<List<Car>>("api/cars"), "Id", "PlateNumber");
            return View(rental);
        }

        // GET: Rentals/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await client.GetFromJsonAsync<Rental>($"api/rentals/{id}");

            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await client.GetFromJsonAsync<Rental>($"api/rentals/{id}");
            if (rental != null)
            {
                var response = await client.DeleteAsync($"api/rentals/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(string.Empty, "Server error, please try again later.");
            return View(rental);
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
