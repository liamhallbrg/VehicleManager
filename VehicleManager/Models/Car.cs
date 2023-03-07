using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleManager.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public int VehicleCategoryId { get; set; }
        [Required]
        public string Brand { get; set; } = string.Empty;
        [Required]
        public string Model { get; set; } = string.Empty;
        [MaxLength (500)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        [DisplayName("Plate Number")]
        public string PlateNumber { get; set; } = string.Empty;
        public string ImgUrl { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/1/1d/Opel_Kadett_C_Coupe.jpg";
    }
}
