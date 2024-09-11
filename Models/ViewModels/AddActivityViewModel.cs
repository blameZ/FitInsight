using System;
using System.ComponentModel.DataAnnotations;

namespace FitInsight.Models.ViewModels
{
    public class AddActivityViewModel
    {
        [Required(ErrorMessage = "Wprowadź nazwę aktywności")]
        public string ActivityType { get; set; }

        [Required(ErrorMessage = "Wprowadź dystans")]
        public float Distance { get; set; }

        [Required(ErrorMessage = "Wprowadź czas trwania")]
        public TimeSpan Duration { get; set; }

        [Required(ErrorMessage = "Wprowadź spalone kalorie")]
        public float CaloriesBurned { get; set; }

        [Required(ErrorMessage = "Wprowadź czas rozpoczęcia")]
        public DateTime StartTime { get; set; }
    }
}

