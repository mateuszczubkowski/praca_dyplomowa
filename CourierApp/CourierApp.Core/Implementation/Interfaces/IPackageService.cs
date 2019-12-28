﻿using System;
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

        Task ChangeStatus(int id, PackageStatus status);

        Task Create(CreatePackageViewModel model);

        Task<IEnumerable<AllPackagesListViewModel>> GetAllPackages();

        Task<int> CheckStatus(CheckPackageStatusViewModel model);
    }
}
