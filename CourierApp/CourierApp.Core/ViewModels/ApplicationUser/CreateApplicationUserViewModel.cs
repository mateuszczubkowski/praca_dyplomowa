using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourierApp.Core.ViewModels.ApplicationUser
{
    public class CreateApplicationUserViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
