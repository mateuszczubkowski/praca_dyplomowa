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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateApplicationUserViewModel {Roles = _userService.GetRoles()};

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateApplicationUserViewModel model)
        {
            int courierId = 0;

            if (!ModelState.IsValid)
            {
                model.Roles = _userService.GetRoles();
                return View(model);
            }

            if (model.Role == "Courier")
            {
                courierId = await _courierService.AddCourier(new CreateCourierViewModel()
                {
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                });
            }

            var identityUser = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                CourierId = courierId 
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByEmailAsync(identityUser.Email);

                await _userManager.AddToRoleAsync(currentUser, model.Role);

            }

            return RedirectToAction("Index", "Home");
        }


    }
}