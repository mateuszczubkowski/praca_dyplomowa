using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.Core.Enums;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Packages;
using Microsoft.AspNetCore.Mvc;

namespace CourierApp.WebApp.Controllers
{
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

        public async Task<IActionResult> ChangeStatus(int id, int courierId)
        {
            await _packageService.ChangeStatus(id, PackageStatus.Delivered);

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
    }
}