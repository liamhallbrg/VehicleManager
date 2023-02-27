namespace VehicleManager.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public int VehicleCategoryId { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string ImgUrl { get; set; } = "https://upload.wikimedia.org/wikipedia/commons/1/1d/Opel_Kadett_C_Coupe.jpg";
        public DateTime RentalStart { get; set; }
        public DateTime RentalEnd { get; set; }
    }
}
