using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.Core.Enums;
using CourierApp.Core.Implementation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourierApp.WebApp.Controllers
{
    public class PackageController : Controller
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        public async Task<IActionResult> PackageList(int id)
        {
            var result = await _packageService.GetPackages(id);

            return View(result);
        }

        public async Task<IActionResult> ChangeStatus(int id)
        {
            await _packageService.ChangeStatus(id, PackageStatus.Delivered);

            return RedirectToAction("PackageList");
        }
    }
}