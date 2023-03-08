﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleManager.Models
{
	public class Customer
	{
		[DisplayName("Id")]
		public int CustomerId { get; set; }
		[Required]
		[MaxLength(40)]
		[DisplayName("First name")]
		public string FirstName { get; set; } = string.Empty;
		[Required]
		[MaxLength(40)]
        [DisplayName("Last name")]
        public string LastName { get; set; } = string.Empty;
		public string FullName => $"{FirstName} {LastName}";
		[Required]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string Address { get; set; } = string.Empty;
		[Required]
		public string City { get; set; } = string.Empty;
		[Required]
		[Range(100000000, 999999999,ErrorMessage ="You must enter a valid 9 digit drivers licence")]
		[DisplayName("License nr")]
        public int DriverLicenceNr { get; set; }

    }
}
