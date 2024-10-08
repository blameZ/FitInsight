﻿using System;
using FitInsight.Models.ActivityModels.cs;

namespace FitInsight.Models.ViewModels
{
	public class UserInfoViewModel
	{
		public string UserName { get; set; }
		public Guid UserId { get; set; }
		public string Email { get; set; }
		public int TotalActivities { get; set; }
		public IEnumerable<Activity> RecentActivities { get; set; }
		public IEnumerable<ActivityViewModel> AllActivities { get; set; }
		public DateTime? StartDateFilter { get; set; }
		public DateTime? EndDateFilter { get; set; }
	}
}