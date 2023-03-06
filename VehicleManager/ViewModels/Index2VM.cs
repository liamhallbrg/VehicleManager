using VehicleManager.Models;

namespace VehicleManager.ViewModels
{
    public class Index2VM
    {
        public List<Car> Cars { get; set; } = new();
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public double TotalPrice { get; set; }
    }
}
