using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitInsight.Models.ActivityModels.cs;

namespace FitInsight.Models.ActivityModels
{
	public class ActivityRoute
	{
        [Key]
        public int RouteId { get; set; }

        [Required]
        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public float Altitude { get; set; }

        public DateTime RecordedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

