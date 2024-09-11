using System;
using Microsoft.AspNetCore.Identity;

namespace FitInsight.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public float? CurrentWeight { get; set; }
	}
}