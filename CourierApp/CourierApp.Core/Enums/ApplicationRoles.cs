using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CourierApp.Core.Enums
{
    enum ApplicationRoles
    {
        [Display(Name = "Administrator")]
        Admin,
        [Display(Name = "Kurier")]
        Courier,
        [Display(Name = "Manager")]
        Manager,
        [Display(Name = "Magazynier")]
        Warehouseman
    }
}
