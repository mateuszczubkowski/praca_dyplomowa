using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.Enums;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Packages;
using CourierApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CourierApp.Core.Implementation
{
    public class PackageService : IPackageService
    {
        private readonly CourierAppDbContext _dbContext;

        public PackageService(CourierAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PackagesListViewModel>> GetPackages(int courierId)
        {
            return await _dbContext.Packages.Where(x => x.CourierId == courierId).Select(x => new PackagesListViewModel
            {
                Address = x.Address,
                CustomerEmail = x.CustomerEmail,
                Id = x.Id,
                Status = x.Status
            }).AsNoTracking().ToListAsync();
        }

        public async Task ChangeStatus(int id, PackageStatus status)
        {
            var package = await _dbContext.Packages.FirstOrDefaultAsync(x => x.Id == id);

            package.Status = status.ToString();

            await _dbContext.SaveChangesAsync();
        }
    }
}
