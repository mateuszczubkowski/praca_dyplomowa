using System;
using System.Collections.Generic;
using System.Text;

namespace CourierApp.Core.ViewModels.Packages
{
    public class GetPackagesListsViewModel
    {
        public IEnumerable<PackagesListViewModel> Delivered { get; set; }

        public IEnumerable<PackagesListViewModel> InProgress { get; set; }
    }
}
