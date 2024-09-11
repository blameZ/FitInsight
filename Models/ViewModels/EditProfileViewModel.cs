using System;
using System.ComponentModel;

namespace FitInsight.Models.ViewModels
{
    public class EditProfileViewModel
    {
        [DisplayName("Imię")]
        public string FirstName { get; set; }

        [DisplayName("Nazwisko")]
        public string LastName { get; set; }

        [DisplayName("Nazwa użytkownika")]
        public string UserName { get; set; }

        [DisplayName("E-Mail")]
        public string Email { get; set; }

        [DisplayName("Wiek")]
        public int Age { get; set; }
    }
}

