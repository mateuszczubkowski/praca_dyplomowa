using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourierApp.Core.ViewModels.Packages
{
    public class ChangePackageCourierViewModel
    {
        public IEnumerable<PackageWithoutCourierViewModel> Packages { get; set; }

        public IEnumerable<SelectListItem> Couriers { get; set; }

        [Display(Name = "Kurier")]
        public int CourierId { get; set; }
    }
}
