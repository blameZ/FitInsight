using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FitInsight.Models
{
    public class AccountViewModel
    {
        [Required(ErrorMessage = "Pole adres e-mail jest wymagane.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu e-mail.")]
        [DisplayName("Adres e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole hasło jest wymagane.")]
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pole adres Imię jest wymagane.")]
        [DisplayName("Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole adres Nazwisko jest wymagane.")]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pole adres wiek jest wymagane.")]
        [DisplayName("Wiek")]
        public int Age { get; set; }
    }
}
