
using System;
namespace CustomerNetApp.Models
{
	public class Customer
	{
        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdated { get; set; }

        public Guid Id { get; set; }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public string? Phone { get; set; }

		public string? Email { get; set; }


    }
}

