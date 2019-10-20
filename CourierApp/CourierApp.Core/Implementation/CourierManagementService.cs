using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Data;
using CourierApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierApp.Core.Implementation
{
    public class CourierManagementService : ICourierManagementService
    {
        private readonly CourierAppDbContext _dbContext;

        public CourierManagementService(CourierAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddCourier()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Courier>> GetCouriersList()
        {
            return await _dbContext.Couriers.AsNoTracking().ToListAsync();
        }

        public Task GetCourier(int id)
        {
            throw new NotImplementedException();
        }
    }
}
