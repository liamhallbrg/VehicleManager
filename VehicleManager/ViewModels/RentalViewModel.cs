
using System.ComponentModel.DataAnnotations;
using VehicleManager.Models;

namespace VehicleManager.ViewModels
{
    public class RentalViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PickUpDate { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public List<Rental> AllRentals { get; set; } = new();
    }
}
