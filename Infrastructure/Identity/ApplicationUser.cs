using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
	public class ApplicationUser : IdentityUser 
	{
		public string DisplayName { get; set; }
		public Address address { get; set; }
	}

	public class Address 
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		[Required]
		public string ApplicationUserId { get; set; }
		public ApplicationUser AppUser { get; set; }
	}
}
