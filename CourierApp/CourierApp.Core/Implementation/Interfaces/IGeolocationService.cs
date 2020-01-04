using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.ViewModels;

namespace CourierApp.Core.Implementation.Interfaces
{
    public interface IGeolocationService
    {
        Task CreateLocation(GeolocationViewModel model);

        Task GetLocation(GeolocationViewModel model);


    }
}
