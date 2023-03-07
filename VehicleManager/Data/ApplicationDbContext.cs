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
                    Name = "Sedan",
                    Description = "desc",
                    PricePerDay = 5000,
                },
                new VehicleCategory
                {
                    VehicleCategoryId = 3,
                    Name = "Sport",
                    Description = "desc",
                    PricePerDay = 5000,
                },
                new VehicleCategory
                {
                    VehicleCategoryId = 4,
                    Name = "Pickup Truck",
                    Description = "desc",
                    PricePerDay = 5000,
                },
                new VehicleCategory
                {
                    VehicleCategoryId = 5,
                    Name = "Minivan",
                    Description = "desc",
                    PricePerDay = 5000,
                }
                );
            modelBuilder.Entity<Car>().HasData(
                //SUV
                new Car
                {
                    CarId = 1, VehicleCategoryId = 1, Brand = "Kia", Model = "Sorento", Description = "Car description",
                    ImgUrl = "https://assets-clean.local-car-finder.com/images/15316/15316_st1280_089.png",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 7, VehicleCategoryId = 1, Brand = "Toyota", Model = "Fortuner", Description = "Car description",
                    ImgUrl = "https://cdni.autocarindia.com/utils/imageresizer.ashx?n=https://cms.haymarketindia.net/model/uploads/modelimages/Toyota-Fortuner-110120211829.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 8, VehicleCategoryId = 1, Brand = "Audi", Model = "Q3 Quattro", Description = "Car description",
                    ImgUrl = "https://inventory.dealersocket.com/api/photo/ZAltwpW4/1024x0/1675973999/u/ecl/RU1k/dCe4/c1eG/6Z1Q/1kKv/cQ.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 9, VehicleCategoryId = 1, Brand = "Hummer", Model = "H2", Description = "Car description",
                    ImgUrl = "https://stimg.cardekho.com/images/carexteriorimages/930x620/Hummer/Hummer-H2/321/1561092676445/front-left-side-47.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 10, VehicleCategoryId = 1, Brand = "Porsche", Model = "Cayenne", Description = "Car description",
                    ImgUrl = "https://imgd.aeplcdn.com/1280x720/cw/ec/32951/Porsche-Cayenne-Exterior-138359.jpg?wm=0&q=75",
                    PlateNumber = "abc123"
                },

                //Sedan
                new Car
                {
                    CarId = 2, VehicleCategoryId = 2, Brand = "Kia", Model = "Cadenza", Description = "Car description",
                    ImgUrl = "https://www.motortrend.com/uploads/izmo/kia/cadenza/2020/cadenza.png",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 3, VehicleCategoryId = 2, Brand = "Toyota", Model = "Camry", Description = "Car description",
                    ImgUrl = "https://imgd.aeplcdn.com/1200x900/n/cw/ec/110233/2022-camry-exterior-right-front-three-quarter.jpeg?isig=0&q=75",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 11, VehicleCategoryId = 2, Brand = "Honda", Model = "Accord", Description = "Car description",
                    ImgUrl = "https://imgd.aeplcdn.com/0x0/cw/ec/21613/Honda-Accord-Right-Front-Three-Quarter-82683.jpg?v=201711021421",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 12, VehicleCategoryId = 2, Brand = "Mercedes-Benz", Model = "S-Class", Description = "Car description",
                    ImgUrl = "https://www.mercedes-benz.se/passengercars/mercedes-benz-cars/s-class/range-overview/_jcr_content/swipeableteaserbox/par/swipeableteaser/asset.MQ6.12.20210602153015.jpeg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 13, VehicleCategoryId = 2, Brand = "BMW", Model = "5-Series", Description = "Car description",
                    ImgUrl = "https://www.bmw.in/content/dam/bmw/marketIN/bmw_in/all-models/5-series/2020/Interior-andexterior/530d-M-Sport-Front-Quarter.jpg.asset.1623071674609.jpg",
                    PlateNumber = "abc123"
                },

                //Sport

                new Car
                {
                    CarId = 4, VehicleCategoryId = 3, Brand = "Kia", Model = "Stinger", Description = "Car description",
                    ImgUrl = "https://m.atcdn.co.uk/vms/media/e3821c59c01441719e67442360718ecf.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 14, VehicleCategoryId = 3, Brand = "Porsche", Model = "911", Description = "Car description",
                    ImgUrl = "https://tdrresearch.azureedge.net/photos/chrome/Expanded/White/2021PRC010103/2021PRC01010301.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 15, VehicleCategoryId = 3, Brand = "Chevrolet", Model = "Corvette", Description = "Car description",
                    ImgUrl = "https://www.chevrolet.com.mx/content/dam/chevrolet/na/mx/es/index/performance/2023-corvette-stingray/colorizer/jellys/black.jpg?imwidth=960",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 16, VehicleCategoryId = 3, Brand = "Audi", Model = "R8", Description = "Car description",
                    ImgUrl = "https://mediaservice.audi.com/media/fast/H4sIAAAAAAAAAFvzloG1tIiBOTrayfuvpGh6-m1zJgaGigIGBgZGoDhTtNOaz-I_2DhCHkCFmZQZWMpTkwoYgSrcmLgycxPTU_VBArz____nZ-QustBNrSjRzUvNzWcHKhHkiZsfe9Uw6_id7Q-9vswPcnnBfueZOAOP5I12FrNLew-2znNlmMC35scXWVYfBh4zex33VgEGr7gsDulu9xl7Hy6oZWbg4TW-PefHP5aZep8faqne937GscFNm4GnVCPogbFQwfYPGpHX08-uzWD6YTeLgefYQuU3z-9lz7BZlM9wmm_Jvw_P_gLNPWW3dm-h6SrFcv1NL7ZpNVZ2lnyZDRR1mn31tOCp33K3n23XmLfm_gmWKCEGnh-taY8Y_cOTDgioMH91rql2WjYtl4GnXUGyQu7CH6-ENxdffdr5nrln8ROgGx7dWV_Cxbpjq0tetNre7_7Ol-WehzHwHLW7m2XMH80we60ZQ9XfLrM-kRdTGXj2Vij-ZomzELj55MTlzr8nZdOvWwgy8Bhlq3z6UJ0htC89XDRI4dsuj_feTgw8c45a_p6440W6hirHZ52PJu8rE5awMPAwPNDLqtdewTRZz-jLdqVll3aHLH_OwNN51f1lSnDMTB5pniyPd5PnxTAtPcfACowept1AgiUOSPA8ARIcNgxgEhRxG4AE40IQn7WemYGB24GBgS2EAQT4hEuLcgoSixJz9YpSiwvy84ozy1IFNQyIBMKsPo6RrkEAFfedMlYCAAA?mimetype=image/png",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 17, VehicleCategoryId = 3, Brand = "Nissan", Model = "GTR", Description = "Car description",
                    ImgUrl = "https://www.nissan.se/content/dam/Nissan/nissan_europe/experience_nissan/performance/18tdieu-helios317.jpg",
                    PlateNumber = "abc123"
                },
                //Pickup
                new Car
                {
                    CarId = 5, VehicleCategoryId = 4, Brand = "GMC", Model = "Canyon", Description = "Car description",
                    ImgUrl = "https://inv.assets.sincrod.com/RTT/GMC/2023/5959513/default/ext_GAZ_deg02.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 18, VehicleCategoryId = 4, Brand = "Ford", Model = "F-150", Description = "Car description",
                    ImgUrl = "https://di-uploads-pod14.dealerinspire.com/kingsford/uploads/2020/06/2021-Ford-F-150-left-1.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 19, VehicleCategoryId = 4, Brand = "Chevrolet", Model = "Silverado", Description = "Car description",
                    ImgUrl = "https://www.hsv.com.au/images/see/silverado/Silverado-2500HD-LTZ-Midnight-Edition-Black.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 20, VehicleCategoryId = 4, Brand = "RAM", Model = "1500", Description = "Car description",
                    ImgUrl = "https://aeceurope.com/wp-content/uploads/2022/07/ram_trx_flame_red_x_black.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 21, VehicleCategoryId = 4, Brand = "Toyota ", Model = "Tacoma", Description = "Car description",
                    ImgUrl = "https://www.toyotaofnewbern.com/assets/stock/colormatched_01/transparent/1280/cc_2023tot09_01_1280/cc_2023tot090074_01_1280_040.png?bg-color=FFFFFF",
                    PlateNumber = "abc123"
                },
                //Minivan

                new Car
                {
                    CarId = 6, VehicleCategoryId = 5, Brand = "Kia", Model = "Sedona", Description = "Car description",
                    ImgUrl = "https://di-uploads-pod12.dealerinspire.com/beavertonkiaredesign/uploads/2019/04/white-kia-sedona-cc0998a8.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 22, VehicleCategoryId = 5, Brand = "Chrysler", Model = "Pacifica", Description = "Car description",
                    ImgUrl = "https://medias.fcacanada.ca/jellies/renditions/2023/800x510/CC23_RUCS53_2DS_PAU_APA_XXX_XXX_XXX.ec6a769ba13c51aaf9074a442629fe4c.png",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 23, VehicleCategoryId = 5, Brand = "Honda", Model = "Odyssey", Description = "Car description",
                    ImgUrl = "https://www.honda.ca/-/media/Brands/Honda/Models/ODYSSEY/2023/Overview/03_Key-Features/desktop/Honda_Odyssey_key-features_desktop_1036x520.png?h=520&iar=0&w=1036&rev=fb3f14bd71a94de697e91e783024600a&hash=33043FE0D7B4FD01D4C775BD095CFEED",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 24, VehicleCategoryId = 5, Brand = "Toyota", Model = "Sienna", Description = "Car description",
                    ImgUrl = "https://di-uploads-pod28.dealerinspire.com/colonialtoyota/uploads/2021/11/2022-Toyota-Sienna-LE-.jpg",
                    PlateNumber = "abc123"
                },
                new Car
                {
                    CarId = 25, VehicleCategoryId = 5, Brand = "Dodge ", Model = "Grand Caravan", Description = "Car description",
                    ImgUrl = "https://images.hgmsites.net/lrg/2010-dodge-grand-caravan-4-door-wagon-sxt-angular-front-exterior-view_100252340_l.jpg",
                    PlateNumber = "abc123"
                }
                ) ;

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@gmail.com", Address = "123 Main St", City = "Anytown", DriverLicenceNr = 123456789 },
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Doe", Email = "janedoe@gmail.com", Address = "456 Maple Ave", City = "Othertown", DriverLicenceNr = 987654321 },
                new Customer { CustomerId = 3, FirstName = "Bob", LastName = "Smith", Email = "bobsmith@gmail.com", Address = "789 Oak St", City = "Somewhere", DriverLicenceNr = 456123789 },
                new Customer { CustomerId = 4, FirstName = "Alice", LastName = "Jones", Email = "alicejones@gmail.com", Address = "456 Elm St", City = "Anywhere", DriverLicenceNr = 789123456 },
                new Customer { CustomerId = 5, FirstName = "Michael", LastName = "Johnson", Email = "michaeljohnson@gmail.com", Address = "789 Pine St", City = "Nowhere", DriverLicenceNr = 123789456 },
                new Customer { CustomerId = 6, FirstName = "Emily", LastName = "Davis", Email = "emilydavis@gmail.com", Address = "123 Cedar St", City = "Someplace", DriverLicenceNr = 987654321 },
                new Customer { CustomerId = 7, FirstName = "David", LastName = "Brown", Email = "davidbrown@gmail.com", Address = "456 Oak St", City = "Anywhere", DriverLicenceNr = 654321789 },
                new Customer { CustomerId = 8, FirstName = "Olivia", LastName = "Miller", Email = "oliviamiller@gmail.com", Address = "789 Cedar St", City = "Somewhere", DriverLicenceNr = 321456987 },
                new Customer { CustomerId = 9, FirstName = "James", LastName = "Wilson", Email = "jameswilson@gmail.com", Address = "123 Pine St", City = "Nowhere", DriverLicenceNr = 789456123 },
                new Customer { CustomerId = 10, FirstName = "Sophia", LastName = "Moore", Email = "sophiamoore@gmail.com", Address = "456 Elm St", City = "Someplace", DriverLicenceNr = 654789321 },
                new Customer { CustomerId = 11, FirstName = "William", LastName = "Taylor", Email = "williamtaylor@gmail.com", Address = "789 Cedar St", City = "Anywhere", DriverLicenceNr = 123456789 },
                new Customer { CustomerId = 12, FirstName = "Emma", LastName = "Anderson", Email = "emmaanderson@gmail.com", Address = "123 Oak St", City = "Somewhere", DriverLicenceNr = 321789456 },
                new Customer { CustomerId = 13, FirstName = "Charles", LastName = "Johnson", Email = "charlesjohnson@gmail.com", Address = "456 Pine St", City = "Nowhere", DriverLicenceNr = 987654321 },
                new Customer { CustomerId = 14, FirstName = "Ava", LastName = "Wilson", Email = "avawilson@gmail.com", Address = "789 Cedar St", City = "Someplace", DriverLicenceNr = 456789123 },
                new Customer { CustomerId = 15, FirstName = "Alexander", LastName = "Martin", Email = "alexandermartin@gmail.com", Address = "123 Elm St", City = "Anywhere", DriverLicenceNr = 789123456 },
                new Customer { CustomerId = 16, FirstName = "Mia", LastName = "Clark", Email = "miaclark@gmail.com", Address = "456 Cedar St", City = "Somewhere", DriverLicenceNr = 123789456 }

            );

            modelBuilder.Entity<Rental>().HasData(
                new Rental { RentalId = 1, CarId = 1, CustomerId = 1, PickUpDate = DateTime.Now.AddDays(1), ReturnDate = DateTime.Now.AddDays(5), TotalPrice = 245 },
                new Rental { RentalId = 2, CarId = 2, CustomerId = 2, PickUpDate = DateTime.Now.AddDays(2), ReturnDate = DateTime.Now.AddDays(6), TotalPrice = 354 },
                new Rental { RentalId = 3, CarId = 3, CustomerId = 3, PickUpDate = DateTime.Now.AddDays(3), ReturnDate = DateTime.Now.AddDays(7), TotalPrice = 483 },
                new Rental { RentalId = 4, CarId = 1, CustomerId = 1, PickUpDate = DateTime.Now.AddDays(100), ReturnDate = DateTime.Now.AddDays(101), TotalPrice = 483 },
                new Rental { RentalId = 5, CarId = 2, CustomerId = 2, PickUpDate = DateTime.Now.AddDays(200), ReturnDate = DateTime.Now.AddDays(201), TotalPrice = 483 },
                new Rental { RentalId = 6, CarId = 3, CustomerId = 3, PickUpDate = DateTime.Now.AddDays(4), ReturnDate = DateTime.Now.AddDays(8), TotalPrice = 531 },
                new Rental { RentalId = 7, CarId = 4, CustomerId = 4, PickUpDate = DateTime.Now.AddDays(5), ReturnDate = DateTime.Now.AddDays(9), TotalPrice = 608 },
                new Rental { RentalId = 8, CarId = 5, CustomerId = 5, PickUpDate = DateTime.Now.AddDays(6), ReturnDate = DateTime.Now.AddDays(10), TotalPrice = 704 },
                new Rental { RentalId = 9, CarId = 6, CustomerId = 6, PickUpDate = DateTime.Now.AddDays(7), ReturnDate = DateTime.Now.AddDays(11), TotalPrice = 846 },
                new Rental { RentalId = 10, CarId = 7, CustomerId = 7, PickUpDate = DateTime.Now.AddDays(8), ReturnDate = DateTime.Now.AddDays(12), TotalPrice = 923 },
                new Rental { RentalId = 11, CarId = 8, CustomerId = 8, PickUpDate = DateTime.Now.AddDays(9), ReturnDate = DateTime.Now.AddDays(13), TotalPrice = 1020 },
                new Rental { RentalId = 12, CarId = 9, CustomerId = 9, PickUpDate = DateTime.Now.AddDays(10), ReturnDate = DateTime.Now.AddDays(14), TotalPrice = 1162 },
                new Rental { RentalId = 13, CarId = 10, CustomerId = 10, PickUpDate = DateTime.Now.AddDays(11), ReturnDate = DateTime.Now.AddDays(15), TotalPrice = 1239 },
                new Rental { RentalId = 14, CarId = 11, CustomerId = 11, PickUpDate = DateTime.Now.AddDays(12), ReturnDate = DateTime.Now.AddDays(16), TotalPrice = 1381 },
                new Rental { RentalId = 15, CarId = 12, CustomerId = 12, PickUpDate = DateTime.Now.AddDays(13), ReturnDate = DateTime.Now.AddDays(17), TotalPrice = 1484 },
                new Rental { RentalId = 16, CarId = 13, CustomerId = 13, PickUpDate = DateTime.Now.AddDays(14), ReturnDate = DateTime.Now.AddDays(18), TotalPrice = 1626 },
                new Rental { RentalId = 17, CarId = 14, CustomerId = 14, PickUpDate = DateTime.Now.AddDays(15), ReturnDate = DateTime.Now.AddDays(19), TotalPrice = 1852 },
                new Rental { RentalId = 18, CarId = 15, CustomerId = 15, PickUpDate = DateTime.Now.AddDays(16), ReturnDate = DateTime.Now.AddDays(20), TotalPrice = 2019 },
                new Rental { RentalId = 19, CarId = 16, CustomerId = 16, PickUpDate = DateTime.Now.AddDays(17), ReturnDate = DateTime.Now.AddDays(21), TotalPrice = 2186 },
                new Rental { RentalId = 20, CarId = 1, CustomerId = 2, PickUpDate = DateTime.Now.AddDays(18), ReturnDate = DateTime.Now.AddDays(22), TotalPrice = 189 },
                new Rental { RentalId = 21, CarId = 2, CustomerId = 3, PickUpDate = DateTime.Now.AddDays(19), ReturnDate = DateTime.Now.AddDays(23), TotalPrice = 327 },
                new Rental { RentalId = 22, CarId = 3, CustomerId = 4, PickUpDate = DateTime.Now.AddDays(20), ReturnDate = DateTime.Now.AddDays(24), TotalPrice = 453 },
                new Rental { RentalId = 23, CarId = 4, CustomerId = 5, PickUpDate = DateTime.Now.AddDays(21), ReturnDate = DateTime.Now.AddDays(25), TotalPrice = 571 },
                new Rental { RentalId = 24, CarId = 5, CustomerId = 6, PickUpDate = DateTime.Now.AddDays(22), ReturnDate = DateTime.Now.AddDays(26), TotalPrice = 708 },
                new Rental { RentalId = 25, CarId = 6, CustomerId = 7, PickUpDate = DateTime.Now.AddDays(23), ReturnDate = DateTime.Now.AddDays(27), TotalPrice = 835 }


            );
        }


        public DbSet<VehicleManager.Models.Car> Cars { get; set; } = default!;
        public DbSet<VehicleManager.Models.VehicleCategory> VehicleCategories { get; set; } = default!;
        public DbSet<VehicleManager.Models.Rental> Rentals { get; set; } = default!;
        public DbSet<VehicleManager.Models.Customer> Customer { get; set; } = default!;
      

    }
}
                                       