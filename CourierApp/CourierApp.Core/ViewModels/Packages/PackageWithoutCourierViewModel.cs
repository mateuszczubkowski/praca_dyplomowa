using System;
using System.Collections.Generic;
using System.Text;

namespace CourierApp.Core.ViewModels.Packages
{
    public class PackageWithoutCourierViewModel
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string CustomerEmail { get; set; }

        public string Status { get; set; }

        public bool Check { get; set; }
    }
}
