using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Data.Models;

namespace CourierApp.Core.Implementation.Interfaces
{
    public interface ICourierManagementService
    {
        Task AddCourier();

        Task<IEnumerable<Courier>> GetCouriersList();

        Task GetCourier(int id);
    }
}
