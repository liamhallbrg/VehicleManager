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
        public double PricePerDay { get; set; }
        virtual public List<Car>? Cars { get; set; }
    }
}
