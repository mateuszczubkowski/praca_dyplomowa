using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourierApp.Core.ViewModels.Packages
{
    public class CheckPackageStatusViewModel
    {
        [Display(Name = "Adres e-mail")]
        public string Mail { get; set; }

        [Display(Name = "Numer przesyłki")]
        public int Id { get; set; }

        public int Status { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Key { get; set; }
    }
}
