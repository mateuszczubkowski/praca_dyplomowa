using System;
using System.Collections.Generic;
using System.Text;
using CourierApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourierApp.Data
{
    public class CourierAppIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public CourierAppIdentityDbContext(DbContextOptions<CourierAppIdentityDbContext> options) : base(options)
        {
                
        }
    }
}
