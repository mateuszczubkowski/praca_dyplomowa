using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourierApp.Core.ViewModels.Review
{
    public class CreateReviewViewModel
    {
        [Display(Name = "Treść")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Ocena jest wymagana")]
        [Display(Name = "Ocena")]
        public int Mark { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres e-mail")]
        [Display(Name = "Adres e-mail")]
        public string Author { get; set; }

        public string Link { get; set; }
    }
}
