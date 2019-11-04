using System.ComponentModel.DataAnnotations;

namespace CourierApp.Core.ViewModels.Courier
{
    public class CreateCourierViewModel
    {
        [Required(ErrorMessage = "Imię jest wymagane")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [Display(Name = "Nazwisko")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [Phone(ErrorMessage = "Niepoprawny nr telefonu")]
        [Display(Name = "Nr telefonu")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres e-mail")]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }
    }
}
