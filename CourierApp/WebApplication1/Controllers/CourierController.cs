using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CourierList()
        {
            var result = await _courierManagement.GetCouriersList();

            return View(result);
        }
    }
}