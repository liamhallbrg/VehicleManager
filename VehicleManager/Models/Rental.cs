using System.ComponentModel.DataAnnotations;

namespace VehicleManager.Models
{
	public class Rental
	{
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PickUpDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReturnDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BookingMade { get; set; } = DateTime.Now;
        public double TotalPrice { get; set; }
        public bool ReadyForPickUp { get; set; } = false;


    }
}
