using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleManager.Models
{
	public class Rental
	{
        public int RentalId { get; set; }
        public int CarId { get; set; }
        [DisplayName("Customer id")]
        public int CustomerId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PickUpDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReturnDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Booking made")]
        public DateTime BookingMade { get; set; } = DateTime.Now;
        public double TotalPrice { get; set; }
        public bool ReadyForPickUp { get; set; } = false;


    }
}
