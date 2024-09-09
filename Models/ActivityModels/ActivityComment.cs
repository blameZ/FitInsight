using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitInsight.Models.ActivityModels.cs;

namespace FitInsight.Models.ActivityModels
{
	public class ActivityComment
	{
        [Key]
        public int CommentId { get; set; }

        [Required]
        public int ActivityId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }

        [Required]
        [MaxLength(500)]
        public string CommentText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

