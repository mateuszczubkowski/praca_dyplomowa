using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.ApplicationUser;
using CourierApp.Core.ViewModels.Courier;
using CourierApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourierApp.WebApp.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ApplicationUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserService _userService;
        private readonly ICourierManagementService _courierService;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, IApplicationUserService userService, ICourierManagementService courierService)
        {
            _userManager = userManager;
            _userService = userService;
            _courierService = courierService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateApplicationUserViewModel {Roles = _userService.GetRoles()};

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateApplicationUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Roles = _userService.GetRoles();
                return View(model);
            }

            await _userService.Create(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var model = await _userService.GetUsers();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var model = new ChangePasswordViewModel()
            {
                UserId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            await _userService.ChangeUserPassword(model);

            return RedirectToAction("GetUsers");
        }
    }
}