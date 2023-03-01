namespace VehicleManager.ViewModels
{
	public class RentalsForPickUpVM
	{
        public int Id { get; set; }
        public DateTime PickUpDate { get; set; }
        public int PlateNumber { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}
