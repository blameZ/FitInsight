using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitInsight.Models.ActivityModels.cs;

namespace FitInsight.Models.ActivityModels
{
	public class ActivityMetric
	{
        [Key]
        public int MetricId { get; set; }

        [Required]
        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }

        [Required]
        [MaxLength(50)]
        public string MetricType { get; set; }

        public float Value { get; set; }

        public DateTime RecordedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

