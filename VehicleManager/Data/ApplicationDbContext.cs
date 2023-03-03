using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VehicleManager.Models;
using VehicleManager.ViewModels;

namespace VehicleManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleCategory>().HasData(
                new VehicleCategory { VehicleCategoryId = 1, Name = "Economy", PricePerDay = 49 },
                new VehicleCategory { VehicleCategoryId = 2, Name = "Compact", PricePerDay = 59 },
                new VehicleCategory { VehicleCategoryId = 3, Name = "Midsize", PricePerDay = 69 },
                new VehicleCategory { VehicleCategoryId = 4, Name = "Full-size", PricePerDay = 79 }
            );

            modelBuilder.Entity<Car>().HasData(
                new Car { CarId = 1, VehicleCategoryId = 1, Brand = "Toyota", Description = "Economy car", PlateNumber = "ABC123" },
                new Car { CarId = 2, VehicleCategoryId = 2, Brand = "Ford", Description = "Compact car", PlateNumber = "DEF456" },
                new Car { CarId = 3, VehicleCategoryId = 3, Brand = "Honda", Description = "Midsize car", PlateNumber = "GHI789" },
                new Car { CarId = 4, VehicleCategoryId = 4, Brand = "Chevrolet", Description = "Full-size car", PlateNumber = "JKL012" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@gmail.com", Address = "123 Main St", City = "Anytown", DriverLicenceNr = 123456789 },
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Doe", Email = "janedoe@gmail.com", Address = "456 Maple Ave", City = "Othertown", DriverLicenceNr = 987654321 },
                new Customer { CustomerId = 3, FirstName = "Bob", LastName = "Smith", Email = "bobsmith@gmail.com", Address = "789 Oak St", City = "Somewhere", DriverLicenceNr = 456123789 }
            );

            modelBuilder.Entity<Rental>().HasData(
                new Rental { RentalId = 1, CarId = 1, CustomerId = 1, PickUpDate = DateTime.Now.AddDays(1), ReturnDate = DateTime.Now.AddDays(5), TotalPrice = 245 },
                new Rental { RentalId = 2, CarId = 2, CustomerId = 2, PickUpDate = DateTime.Now.AddDays(2), ReturnDate = DateTime.Now.AddDays(6), TotalPrice = 354 },
                new Rental { RentalId = 3, CarId = 3, CustomerId = 3, PickUpDate = DateTime.Now.AddDays(3), ReturnDate = DateTime.Now.AddDays(7), TotalPrice = 483 },
                new Rental { RentalId = 4, CarId = 1, CustomerId = 1, PickUpDate = DateTime.Now.AddDays(100), ReturnDate = DateTime.Now.AddDays(101), TotalPrice = 483 },
                new Rental { RentalId = 5, CarId = 2, CustomerId = 2, PickUpDate = DateTime.Now.AddDays(200), ReturnDate = DateTime.Now.AddDays(201), TotalPrice = 483 }
            );
        }


        public DbSet<VehicleManager.Models.Car> Cars { get; set; } = default!;
        public DbSet<VehicleManager.Models.VehicleCategory> VehicleCategories { get; set; } = default!;
        public DbSet<VehicleManager.Models.Rental> Rentals { get; set; } = default!;
        public DbSet<VehicleManager.Models.Customer> Customer { get; set; } = default!;
      

    }
}
                                       