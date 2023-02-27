namespace VehicleManager.Models
{
    public class VehicleCategory
    {
        public int VehicleCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        virtual public List<Car>? Cars { get; set; }
    }
}
