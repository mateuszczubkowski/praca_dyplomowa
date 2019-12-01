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
        [Required]
        public string Email { get; set; }

        [Required]  
        public string Password { get; set; }

        [RequiredIf("Role", "Courier", "Imię jest wymagane")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [RequiredIf("Role", "Courier", "Nazwisko jest wymagane")]
        [Display(Name = "Nazwisko")]
        public string SecondName { get; set; }

        [RequiredIf("Role", "trCourierue", "Numer telefonu jest wymagany")]
        [Phone(ErrorMessage = "Niepoprawny nr telefonu")]
        [Display(Name = "Nr telefonu")]
        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
