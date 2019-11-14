using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourierApp.WebApp.Controllers
{
    public class ApplicationUserController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private IApplicationUserService _userService;

        public ApplicationUserController(UserManager<IdentityUser> userManager, IApplicationUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
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
            var identityUser = new IdentityUser()
            {
                UserName = model.Email,
                Email = model.Email
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