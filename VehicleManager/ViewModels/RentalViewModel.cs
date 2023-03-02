
namespace VehicleManager.ViewModels
{
    public class RentalViewModel
    {
        public int Id { get; set; }
        public DateTime PickUpDate { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

    }
}
