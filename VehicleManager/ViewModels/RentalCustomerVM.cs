using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleManager.ViewModels
{
    public class RentalCustomerVM
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PickUpDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReturnDate { get; set; }
        public DateTime BookingMade { get; set; } = DateTime.Now;
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double TotalPrice { get; set; }
        public string PlateNumber { get; set; } = string.Empty;


        [Required]
        [MaxLength(40)]
        [DisplayName("First name")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(40)]
        [DisplayName("Last name")]
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        [DisplayName("Driver license")]
        [RegularExpression("^[0-9]{9}$", ErrorMessage = "Driver license number must be 9 digits.")]
        public int DriverLicenceNr { get; set; } = 999999999;

        public string ImgUrl { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/1/1d/Opel_Kadett_C_Coupe.jpg";
        
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double PricePerDay { get; set; }
    }
}
