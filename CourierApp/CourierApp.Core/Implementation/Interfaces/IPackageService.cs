using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.Enums;
using CourierApp.Core.ViewModels.Packages;

namespace CourierApp.Core.Implementation.Interfaces
{
    public interface IPackageService
    {
        Task<IEnumerable<PackagesListViewModel>> GetPackages(int courierId, string status);

        IEnumerable<PackageWithoutCourierViewModel> GetPackages(int courierId, PackageStatus status);

        Task DeliveredPackage(int id);

        Task Create(CreatePackageViewModel model);

        Task<IEnumerable<AllPackagesListViewModel>> GetAllPackages();

        Task<CheckPackageStatusViewModel> CheckStatus(CheckPackageStatusViewModel model);

        Task ChangeCourier(ChangePackageCourierViewModel model);

        Task CollectPackage(ChangePackageCourierViewModel model);
    }
}
