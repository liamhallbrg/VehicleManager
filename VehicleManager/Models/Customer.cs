namespace VehicleManager.Models
{
	public class Customer
	{
		public int CustomerId { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string FullName => $"{FirstName} {LastName}";
		public string Email { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
        public int DriverLicenceNr { get; set; }

    }
}
