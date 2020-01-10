using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.ViewModels.ApplicationUser;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourierApp.Core.Implementation.Interfaces
{
    public interface IApplicationUserService
    {
        IEnumerable<SelectListItem> GetRoles();

        Task<IEnumerable<UserViewModel>> GetUsers();

        Task ChangeUserPassword(ChangePasswordViewModel model);
    }
}
