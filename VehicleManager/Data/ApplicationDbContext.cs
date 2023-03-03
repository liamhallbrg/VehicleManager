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
                new VehicleCategory
                {
                    VehicleCategoryId = 1,
                    Name = "SUV",
                    Description = "desc",
                    PricePerDay = 5000,
                },
                new VehicleCategory
                {
                    VehicleCategoryId = 2,
                    Name = "Hatchback",
                    Description = "desc",
                    PricePerDay = 5000,
                },
                new VehicleCategory
                {
                    VehicleCategoryId = 3,
                    Name = "Sedan",
                    Description = "desc",
                    PricePerDay = 5000,
                },
                new VehicleCategory
                {
                    VehicleCategoryId = 4,
                    Name = "Sport",
                    Description = "desc",
                    PricePerDay = 5000,
                },
                new VehicleCategory
                {
                    VehicleCategoryId = 5,
                    Name = "Pickup Truck",
                    Description = "desc",
                    PricePerDay = 5000,
                },
                new VehicleCategory
                {
                    VehicleCategoryId = 6,
                    Name = "Minivan",
                    Description = "desc",
                    PricePerDay = 5000,
                }
                );
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    CarId = 1,
                    VehicleCategoryId = 1,
                    Brand = "Kia",
                    Description = "Sorento",
                    ImgUrl = "https://www.kia.com/content/dam/kwcms/gt/en/images/discover-kia/voice-search/parts-80-1.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 2,
                    VehicleCategoryId = 2,
                    Brand = "Kia",
                    Description = "Rio",
                    ImgUrl = "https://www.kia.com/content/dam/kwcms/gt/en/images/discover-kia/voice-search/parts-80-2.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 3,
                    VehicleCategoryId = 3,
                    Brand = "Kia",
                    Description = "Cadenza",
                    ImgUrl = "https://www.kia.com/content/dam/kwcms/gt/en/images/discover-kia/voice-search/parts-80-5.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 4,
                    VehicleCategoryId = 4,
                    Brand = "Kia",
                    Description = "Stinger",
                    ImgUrl = "https://www.kia.com/content/dam/kwcms/gt/en/images/discover-kia/voice-search/parts-80-6.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 5,
                    VehicleCategoryId = 5,
                    Brand = "Kia",
                    Description = "Telluride Pickup Truck",
                    ImgUrl = "https://www.kia.com/content/dam/kwcms/gt/en/images/discover-kia/voice-search/parts-80-10.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 6,
                    VehicleCategoryId = 6,
                    Brand = "Kia",
                    Description = "Sedona",
                    ImgUrl = "https://www.kia.com/content/dam/kwcms/gt/en/images/discover-kia/voice-search/parts-80-8.jpg",
                    PlateNumber = "abc123"
                }
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
                                       