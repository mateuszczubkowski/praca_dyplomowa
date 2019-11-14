using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.Core.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourierApp.WebApp.Controllers
{
    public class ApplicationUserController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public ApplicationUserController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //public async Task<IActionResult> Create(CreateApplicationUserViewModel model)
        //{
        //    var identityUser = new IdentityUser()
        //    {
        //        UserName = model.Email,
        //        Email = model.Email
        //    };

        //    var result = await _userManager.CreateAsync(identityUser, model.Password);

        //    if (result.Succeeded)
        //    {
        //        var currentUser = await _userManager.FindByEmailAsync(identityUser.Email);
                
        //        await _userManager.AddToRoleAsync(currentUser, model.Role);
        //    }


        //}
    }
}