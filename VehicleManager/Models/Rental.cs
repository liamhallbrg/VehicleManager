namespace VehicleManager.Models
{
	public class Rental
	{
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int UserId { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime BookingMade { get; set; } = DateTime.Now;
        public int TotalPrice { get; set; }


    }
}
