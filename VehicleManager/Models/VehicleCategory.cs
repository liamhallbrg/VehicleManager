using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleManager.Models
{
    public class VehicleCategory
    {
        public int VehicleCategoryId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength(120)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [DisplayName("Price per day")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double PricePerDay { get; set; }
        virtual public List<Car> Cars { get; set; } = new();
    }
}
