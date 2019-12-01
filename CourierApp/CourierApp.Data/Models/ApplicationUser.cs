using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace CourierApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int CourierId { get; set; }
    }
}
