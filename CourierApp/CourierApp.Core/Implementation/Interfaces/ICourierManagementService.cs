using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.ViewModels.Courier;
using CourierApp.Data.Models;

namespace CourierApp.Core.Implementation.Interfaces
{
    public interface ICourierManagementService
    {
        Task AddCourier();

        Task<IEnumerable<CourierListItemViewModel>> GetCouriersList();

        Task<CourierReviewsDetailsViewModel> GetCourier(int id);
    }
}
