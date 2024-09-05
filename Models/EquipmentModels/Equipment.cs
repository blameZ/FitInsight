using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitInsight.Models.EquipmentModels
{
	public class Equipment
	{
        [Key]
        public int EquipmentId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [MaxLength(100)]
        public string EquipmentName { get; set; }

        [MaxLength(100)]
        public string Brand { get; set; }

        [MaxLength(100)]
        public string Model { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public float? DistanceCovered { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

