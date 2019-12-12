using System.Collections.Generic;
using System.Threading.Tasks;
using CourierApp.Core.ViewModels.Courier;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourierApp.Core.Implementation.Interfaces
{
    public interface ICourierManagementService
    {
        Task<int> AddCourier(CreateCourierViewModel model);

        Task<IEnumerable<CourierListItemViewModel>> GetCouriersList();

        //Task<CourierReviewsDetailsViewModel> GetCourier(int id);

        IEnumerable<SelectListItem> GetCouriers();
    }
}
