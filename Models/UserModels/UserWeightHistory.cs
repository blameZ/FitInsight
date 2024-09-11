using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitInsight.Models.UserModels
{
	public class UserWeightHistory
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string UserId { get; set; }

		public float Weight { get; set; }

		public DateTime Date { get; set; }

		[ForeignKey("UserId")]
		public virtual ApplicationUser User { get; set; } 
	}
}