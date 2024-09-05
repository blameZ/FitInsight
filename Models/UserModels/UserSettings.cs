using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitInsight.Models.UserModels
{
	public class UserSettings
	{
        [Key]
        public int SettingId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [MaxLength(100)]
        public string SettingName { get; set; }

        [MaxLength(255)]
        public string SettingValue { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey("Id")]
        public virtual ApplicationUser User { get; set; }
    }
}

