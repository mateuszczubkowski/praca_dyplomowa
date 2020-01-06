﻿using System;
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
using Org.BouncyCastle.Bcpg;

namespace CourierApp.Core.Implementation
{
    public class PackageService : IPackageService
    {
        private readonly CourierAppDbContext _dbContext;
        private readonly IReviewService _reviewService;
        private readonly IGeolocationService _gpsService;

        public PackageService(CourierAppDbContext dbContext, IReviewService reviewService, IGeolocationService gpsService)
        {
            _dbContext = dbContext;
            _reviewService = reviewService;
            _gpsService = gpsService;
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

        public IEnumerable<PackageWithoutCourierViewModel> GetPackages()
        {
            return _dbContext.Packages.Where(x => x.CourierId == 0 && x.Status == PackageStatus.InMagazine.ToString()).Select(x => new PackageWithoutCourierViewModel()
            {
                Address = x.Address,
                CustomerEmail = x.CustomerEmail,
                Id = x.Id,
                Status = x.Status,
                Check = false
            }).AsNoTracking().ToList();
        }

        public async Task DeliveredPackage(int id)
        {
            var package = await _dbContext.Packages.FirstOrDefaultAsync(x => x.Id == id);

            package.Status = PackageStatus.Delivered.ToString();

            await _dbContext.SaveChangesAsync();

            if (package.CourierId != null)
            {
                var link = _reviewService.CreateReviewLink(package.CourierId);

                await _reviewService.CreateReviewLink(package.CourierId, link, package.CustomerEmail);
                _reviewService.SendReviewLink(package.CustomerEmail, link);
            }
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

            if (model.CourierId == 0)
            {
                package.CourierId = null;
            }

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

        public async Task<CheckPackageStatusViewModel> CheckStatus(CheckPackageStatusViewModel model)
        {
            var package = await _dbContext.Packages.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == model.Id && p.CustomerEmail == model.Mail);

            if (package == null)
            {
                model.Status =  0;
            }
            else
            {
                if (package.Status == PackageStatus.InMagazine.ToString())
                {
                    model.Status = 1;
                }
                else if (package.Status == PackageStatus.InProgress.ToString())
                {
                    model = await _gpsService.GetLocation(model);
                }
                else if (package.Status == PackageStatus.Delivered.ToString())
                {
                    model.Status = 3;
                }
                else
                {
                    model.Status = 0;
                }
            }

            return model;
        }

        public async Task ChangeCourier(ChangePackageCourierViewModel model)
        {
            var packages = new List<PackageWithoutCourierViewModel>();

            Package package;
                
            foreach (var p in model.Packages)
            {
                if (p.Check == true)
                {
                    packages.Add(p);
                }
            }

            foreach (var p in packages)
            {
                package = await _dbContext.Packages.FirstOrDefaultAsync(x => x.Id == p.Id);
                if (package != null)
                {
                    package.CourierId = model.CourierId;

                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
