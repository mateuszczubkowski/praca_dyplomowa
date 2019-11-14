using System;
using System.Collections.Generic;
using System.Text;

namespace CourierApp.Core.ViewModels.ApplicationUser
{
    public class CreateApplicationUserViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
