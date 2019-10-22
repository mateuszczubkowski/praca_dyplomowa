using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Courier;
using CourierApp.Data;
using CourierApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierApp.Core.Implementation
{
    public class CourierManagementService : ICourierManagementService
    {
        private readonly CourierAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CourierManagementService(CourierAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task AddCourier()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CourierListItem>> GetCouriersList()
        {
            var result = new List<CourierListItem>();

            var couriers = await _dbContext.Couriers.AsNoTracking().OrderBy(x => x.SecondName).ToListAsync();

            foreach (var courier in couriers)
            {
                result.Add(_mapper.Map<CourierListItem>(courier));
            }

            return result;
        }

        public Task GetCourier(int id)
        {
            throw new NotImplementedException();
        }
    }
}
