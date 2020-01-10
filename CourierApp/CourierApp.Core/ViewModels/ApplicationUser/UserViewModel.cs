using System;
using System.Collections.Generic;
using System.Text;

namespace CourierApp.Core.ViewModels.ApplicationUser
{
    public class UserViewModel
    {
        public string Email { get; set; }

        public string Id { get; set; }

        public int CourierId { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
