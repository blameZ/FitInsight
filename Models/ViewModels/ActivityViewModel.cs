using System;
using System.ComponentModel.DataAnnotations;
using FitInsight.Models.ActivityModels;

namespace FitInsight.Models.ViewModels
{
	public class ActivityViewModel
	{
        public int ActivityId { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public bool IsPrivate { get; set; }

        public string ActivityType { get; set; }

        public float Distance { get; set; }

        public TimeSpan Duration { get; set; }

        public float CaloriesBurned { get; set; }

        public DateTime StartTime { get; set; }

        public ICollection<ActivityComment> ActivityComments { get; set; }
        public ICollection<ActivityLike> ActivityLikes { get; set; }

    }
}

