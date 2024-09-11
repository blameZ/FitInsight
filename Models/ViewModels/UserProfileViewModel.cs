using System;
using FitInsight.Models.UserModels;

namespace FitInsight.Models.ViewModels
{
	public class UserProfileViewModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public int Age { get; set; }
		public float? CurrentWeight { get; set; }
		public IEnumerable<UserWeightHistory> WeightHistory { get; set; }
	}
}

