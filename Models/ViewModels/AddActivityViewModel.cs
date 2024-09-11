using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FitInsight.Models.ViewModels
{
    public class AddActivityViewModel
    {
        [DisplayName("Nazwa aktywności")]
        [Required(ErrorMessage = "Wprowadź nazwę aktywności")]
        public string ActivityType { get; set; }
        
        [DisplayName("Dystans")]        
        public float Distance { get; set; }

        [DisplayName("Czas trwania")]
        [Required(ErrorMessage = "Wprowadź czas trwania")]
        public TimeSpan Duration { get; set; }

        [DisplayName("Spalone kalorie")]
        [Required(ErrorMessage = "Wprowadź spalone kalorie")]
        public float CaloriesBurned { get; set; }

        [DisplayName("Data rozpoczęcia")]
        [Required(ErrorMessage = "Wprowadź czas rozpoczęcia")]
        public DateTime StartTime { get; set; } 
    }
}

