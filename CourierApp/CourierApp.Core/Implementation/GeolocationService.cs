using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels;
using CourierApp.Core.ViewModels.Packages;
using CourierApp.Data;
using CourierApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourierApp.Core.Implementation
{
    public class GeolocationService : IGeolocationService
    {
        private readonly CourierAppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public GeolocationService(CourierAppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task CreateLocation(GeolocationViewModel model)
        {
            if (ValidatePosition(model.Longitude, model.Latitude))
            {
                if (await _dbContext.CourierPositions.AnyAsync(x => x.CourierId == model.CourierId))
                {
                    await UpdateLocation(model);
                }
                else
                {
                    await CreateNewLocation(model);
                }
            }
        }

        public async Task<CheckPackageStatusViewModel> GetLocation(CheckPackageStatusViewModel model)
        {
            var package = await _dbContext.Packages.AsNoTracking().FirstOrDefaultAsync(p => p.Id == model.Id);
            if (package == null)
            {
                model.Status = 0;

                return model;
            }

            var location = await _dbContext.CourierPositions.AsNoTracking().FirstOrDefaultAsync(x => x.CourierId == package.CourierId);

            if (location == null || (DateTime.Now - location.Date).Hours >= 1)
            {
                model.Status = 4;
                return model;
            }
            else
            {
                model.Longitude = location.Longitude;
                model.Latitude = location.Latitude;
                model.Key = _configuration.GetSection("APIkey").Value;
                model.Status = 2;

                return model;
            }
        }

        private async Task CreateNewLocation(GeolocationViewModel model)
        {
            var location = new CourierPosition()
            {
                CourierId = model.CourierId,
                Date = DateTime.Now,
                Latitude = model.Latitude,
                Longitude = model.Longitude
            };

            await _dbContext.CourierPositions.AddAsync(location);
            await _dbContext.SaveChangesAsync();
        }

        private async Task UpdateLocation(GeolocationViewModel model)
        {
            var location = await _dbContext.CourierPositions.FirstOrDefaultAsync(x => x.CourierId == model.CourierId);

            location.Latitude = model.Latitude;
            location.Longitude = model.Longitude;
            location.Date = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        private bool ValidatePosition(double longitude, double latitude)
        {
            return longitude != 0 && longitude <= 90 && longitude >= -90 &&
                   latitude != 0 && latitude <= 90 && latitude >= -90;
        }
    }
}
