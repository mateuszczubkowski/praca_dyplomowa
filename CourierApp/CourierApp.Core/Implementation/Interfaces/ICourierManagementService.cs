using System.Collections.Generic;
using System.Threading.Tasks;
using CourierApp.Core.ViewModels.Courier;

namespace CourierApp.Core.Implementation.Interfaces
{
    public interface ICourierManagementService
    {
        Task AddCourier(CreateCourierViewModel model);

        Task<IEnumerable<CourierListItemViewModel>> GetCouriersList();

        Task<CourierReviewsDetailsViewModel> GetCourier(int id);
    }
}
