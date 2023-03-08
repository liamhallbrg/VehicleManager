
using System.ComponentModel.DataAnnotations;
using VehicleManager.Models;

namespace VehicleManager.ViewModels
{
    public class RentalViewModel
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PickUpDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReturnDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BookingMade { get; set; }
        public double TotalPrice { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public bool ReadyForPickUp { get; set; } = false;
    }
}
