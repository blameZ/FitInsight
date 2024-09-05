using System;
using FitInsight.Models.ActivityModels.cs;

namespace FitInsight.Models.ViewModels
{
	public class UserInfoViewModel
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public int TotalActivities { get; set; }
		public IEnumerable<Activity> RecentActivities { get; set; }
		public DateTime? StartDateFilter { get; set; }
		public DateTime? EndDateFilter { get; set; }
	}
}

