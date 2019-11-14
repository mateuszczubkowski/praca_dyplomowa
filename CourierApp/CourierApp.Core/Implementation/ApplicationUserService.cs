using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.Enums;
using CourierApp.Core.Implementation.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourierApp.Core.Implementation
{
    public class ApplicationUserService : IApplicationUserService
    {
        public IEnumerable<SelectListItem> GetRoles()
        {
            List<SelectListItem> roles = new List<SelectListItem>();

            foreach (ApplicationRoles role in (ApplicationRoles[]) Enum.GetValues(typeof(ApplicationRoles)))
            {
                roles.Add(new SelectListItem(role.ToString(), role.ToString()));
            }

            return roles;
        }
    }
}
