using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitInsight.Models.ActivityModels.cs
{
	public class Activity
	{
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public bool IsPrivate { get; set; }

        [Required]
        [MaxLength(50)]
        public string ActivityType { get; set; }

        public float Distance { get; set; }

        public TimeSpan Duration { get; set; }

        public float CaloriesBurned { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public ICollection<ActivityMetric> ActivityMetrics { get; set; }
        public ICollection<ActivityRoute> ActivityRoutes { get; set; }
        public ICollection<ActivityComment> ActivityComments { get; set; }
        public ICollection<ActivityLike> ActivityLikes { get; set; }
    }
}

