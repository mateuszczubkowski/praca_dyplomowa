using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.Core.Enums;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Packages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourierApp.WebApp.Controllers
{
    [Authorize]
    public class PackageController : Controller
    {
        private readonly IPackageService _packageService;
        private readonly ICourierManagementService _courierService;

        public PackageController(IPackageService packageService, ICourierManagementService courierService)
        {
            _packageService = packageService;
            _courierService = courierService;
        }

        public async Task<IActionResult> PackageList(int id)
        {
            var result = new GetPackagesListsViewModel()
            {
                Delivered = await _packageService.GetPackages(id, PackageStatus.Delivered.ToString()),
                InProgress = await _packageService.GetPackages(id, PackageStatus.InProgress.ToString())
            };

            return View(result);
        }

        public async Task<IActionResult> AllPackageList()
        {
            var result = await _packageService.GetAllPackages();

            return View(result);
        }

        public async Task<IActionResult> Delivered(int id, int courierId)
        {
            await _packageService.DeliveredPackage(id);

            return RedirectToAction("PackageList", new { id = courierId });
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreatePackageViewModel {Couriers = _courierService.GetCouriers()};

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePackageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _packageService.Create(model);

            return RedirectToAction("Index","Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult CheckStatus()
        {
            var model = new CheckPackageStatusViewModel() {Status = 99};

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CheckStatus(CheckPackageStatusViewModel model)
        {
            var result = await _packageService.CheckStatus(model);

            return View(result);
        }

        [HttpGet]
        public IActionResult ChangePackageCourier()
        {
            var model = new ChangePackageCourierViewModel()
            {
                Couriers = _courierService.GetCouriers(),
                Packages = _packageService.GetPackages(0, PackageStatus.InMagazine)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePackageCourier(ChangePackageCourierViewModel model)
        {
            await _packageService.ChangeCourier(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CollectPackages(int id)
        {
            var model = new ChangePackageCourierViewModel()
            {
                Packages = _packageService.GetPackages(id, PackageStatus.InMagazine),
                CourierId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CollectPackages(ChangePackageCourierViewModel model)
        {
            await _packageService.CollectPackage(model);

            return RedirectToAction("Index", "Home");
        }
    }
}