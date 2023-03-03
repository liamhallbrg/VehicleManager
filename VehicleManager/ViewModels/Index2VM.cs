using VehicleManager.Models;

namespace VehicleManager.ViewModels
{
    public class Index2VM
    {
        public List<Car> Cars { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DropoffDate { get; set; }
    }
}
