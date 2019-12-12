using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourierApp.Core.ViewModels.ApplicationUser
{
    public class CreateApplicationUserViewModel
    {
        [Required(ErrorMessage = "E-mail jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{8,}$", ErrorMessage = "Hasło musi zawierać przynajmniej 8 znaków, w tym przynajmniej 1 wielką literę, 1 małą literę,1 cyfrę oraz 1 znak specjalny.")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [RequiredIf("Role", "Courier", "Imię jest wymagane")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [RequiredIf("Role", "Courier", "Nazwisko jest wymagane")]
        [Display(Name = "Nazwisko")]
        public string SecondName { get; set; }

        [RequiredIf("Role", "Courier", "Numer telefonu jest wymagany")]
        [Phone(ErrorMessage = "Niepoprawny nr telefonu")]
        [Display(Name = "Nr telefonu")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Rola użytkownika")]
        public string Role { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
