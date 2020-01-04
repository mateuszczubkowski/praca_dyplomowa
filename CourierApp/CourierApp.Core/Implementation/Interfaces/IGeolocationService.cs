using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.ViewModels;
using CourierApp.Core.ViewModels.Packages;

namespace CourierApp.Core.Implementation.Interfaces
{
    public interface IGeolocationService
    {
        Task CreateLocation(GeolocationViewModel model);

        Task<CheckPackageStatusViewModel> GetLocation(CheckPackageStatusViewModel model);
    }
}
