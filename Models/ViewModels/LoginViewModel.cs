using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FitInsight.Models.ViewModels
{
	public class LoginViewModel
	{
        [Required(ErrorMessage = "Pole nazwa użytkownika jest wymagane.")]
        [DataType(DataType.Text)]
        [DisplayName("Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Pole hasło jest wymagane.")]
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        public string Password { get; set; }
    }
}

