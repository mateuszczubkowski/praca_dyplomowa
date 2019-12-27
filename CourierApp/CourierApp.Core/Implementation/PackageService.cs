using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.Enums;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Packages;
using CourierApp.Data;
using CourierApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierApp.Core.Implementation
{
    public class PackageService : IPackageService
    {
        private readonly CourierAppDbContext _dbContext;
        private readonly IReviewService _reviewService;

        public PackageService(CourierAppDbContext dbContext, IReviewService reviewService)
        {
            _dbContext = dbContext;
            _reviewService = reviewService;
        }

        public async Task<IEnumerable<PackagesListViewModel>> GetPackages(int courierId, string status)
        {
            return await _dbContext.Packages.Where(x => x.CourierId == courierId && x.Status == status).Select(x => new PackagesListViewModel
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

            var link = _reviewService.CreateReviewLink(package.CourierId);

            await _reviewService.CreateReviewLink(package.CourierId, link, package.CustomerEmail);
            await _reviewService.SendReviewLink(package.CustomerEmail, link);

        }

        public async Task Create(CreatePackageViewModel model)
        {
            var package = new Package()
            {
                Address = model.Address,
                CourierId = model.CourierId,
                CustomerEmail = model.Email,
                Status = PackageStatus.InMagazine.ToString()
            };

            try
            {
                await _dbContext.Packages.AddAsync(package);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

            }
        }

        public async Task<IEnumerable<AllPackagesListViewModel>> GetAllPackages()
        {
            return await _dbContext.Packages.AsNoTracking().Include(x => x.Courier).Select(x =>
                new AllPackagesListViewModel
                {
                    Address = x.Address,
                    Courier = x.Courier != null ? $"{x.Courier.FirstName} {x.Courier.SecondName}" : "Brak kuriera",
                    CustomerEmail = x.CustomerEmail,
                    Id = x.Id,
                    Status = x.Status
                }).ToListAsync();
        }
    }
}
