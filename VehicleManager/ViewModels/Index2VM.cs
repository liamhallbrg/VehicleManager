using System.ComponentModel.DataAnnotations;
using VehicleManager.Models;

namespace VehicleManager.ViewModels
{
    public class Index2VM
    {
        public List<VehicleCategory> VehicleCategories { get; set; } = new();
        public List<Car> Cars { get; set; } = new();
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PickupDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReturnDate { get; set; }
        public VehicleCategory Category { get; set; } = new();
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double TotalPrice { get; set; }
        public int Selected { get; set; }
    }
}
