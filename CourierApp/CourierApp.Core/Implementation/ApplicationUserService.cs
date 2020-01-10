using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.Enums;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.ApplicationUser;
using CourierApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CourierApp.Core.Implementation
{
    public class ApplicationUserService : IApplicationUserService
    {
        public readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IEnumerable<SelectListItem> GetRoles()
        {
            List<SelectListItem> roles = new List<SelectListItem>();

            foreach (ApplicationRoles role in (ApplicationRoles[]) Enum.GetValues(typeof(ApplicationRoles)))
            {
                roles.Add(new SelectListItem(role.GetDisplayName(), role.ToString()));
            }

            return roles;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            var users =  await _userManager.Users.Select(x => new UserViewModel()
            {
                CourierId = x.CourierId,
                Email = x.UserName,
                Id = x.Id
            }).ToListAsync();

            foreach (var user in users)
            {
                var x = await _userManager.FindByIdAsync(user.Id);
                
                user.Roles = await _userManager.GetRolesAsync(x);
            }
            
            return users;
        }

        public async Task ChangeUserPassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, model.Password);
        }
    }
}
