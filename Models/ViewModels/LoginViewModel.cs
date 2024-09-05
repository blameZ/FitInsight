using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FitInsight.Models.ViewModels
{
	public class LoginViewModel
	{
        [Required(ErrorMessage = "Pole adres e-mail jest wymagane.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu e-mail.")]
        [DisplayName("Adres e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole hasło jest wymagane.")]
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        public string Password { get; set; }
    }
}

