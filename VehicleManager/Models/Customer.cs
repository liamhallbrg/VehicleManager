using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleManager.Models
{
	public class Customer
	{
		public int CustomerId { get; set; }
		[Required]
		[MaxLength(40)]
		[DisplayName("Förnamn")]
		public string FirstName { get; set; } = string.Empty;
		[Required]
		[MaxLength(40)]
		public string LastName { get; set; } = string.Empty;
		public string FullName => $"{FirstName} {LastName}";
		[Required]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string Address { get; set; } = string.Empty;
		[Required]
		public string City { get; set; } = string.Empty;
		[Required]
		[Range(100000000, 999999999)]
        public int DriverLicenceNr { get; set; }

    }
}
