using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Courier;
using Microsoft.AspNetCore.Mvc;

namespace CourierApp.WebApp.Controllers
{
    public class CourierController : Controller
    {
        private readonly ICourierManagementService _courierManagement;

        public CourierController(ICourierManagementService courierManagement)
        {
            _courierManagement = courierManagement;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CourierList()
        {
            var result = await _courierManagement.GetCouriersList();

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourierViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _courierManagement.AddCourier(model);

            return RedirectToAction("Index");
        }
    }
}