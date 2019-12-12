using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourierApp.Core.ViewModels.Packages
{
    public class CreatePackageViewModel
    {
        [Required(ErrorMessage = "Adres doręczenia jest wymagany")]
        [Display(Name = "Adres doręczenia")]
        public string Address { get; set; }

        [Required(ErrorMessage = "E-mail klienta jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres e-mail klienta")]
        [Display(Name = "E-mail klienta")]
        public string Email { get; set; }

        [Display(Name = "Kurier")]
        public int CourierId { get; set; }

        public IEnumerable<SelectListItem> Couriers { get; set; }
    }
}
