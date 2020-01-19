using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.Enums;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.ApplicationUser;
using CourierApp.Core.ViewModels.Courier;
using CourierApp.Data.Models;
using CourierApp.MailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CourierApp.Core.Implementation
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICourierManagementService _courierService;
        private readonly MailQueue _mailService;

        public ApplicationUserService(UserManager<ApplicationUser> userManager, ICourierManagementService courierService, MailQueue mailService)
        {
            _userManager = userManager;
            _courierService = courierService;
            _mailService = mailService;
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

        public async Task Create(CreateApplicationUserViewModel model)
        {
            int courierId = 0;

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

                SendPassword(model);
            }
        }
        
        private void SendPassword(CreateApplicationUserViewModel model)
        {
            var message = File.ReadAllText(@"..\WebApplication1\wwwroot\emails\loginMail.html");

            var replace = $"{model.Email}";

            message = message.Replace("#UserLogin#", replace);

            replace = $"{model.Password}";

            message = message.Replace("#UserPassword#", replace);

            var mail = new MailDto()
            {
                Address = model.Email,
                Message = message,
                Subject = "Dane do logowania"
            };

            _mailService.EnqueueMail(mail);
        }
    }
}
