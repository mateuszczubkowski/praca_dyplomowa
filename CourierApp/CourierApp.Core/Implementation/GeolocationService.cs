using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels;
using CourierApp.Data;
using CourierApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierApp.Core.Implementation
{
    public class GeolocationService : IGeolocationService
    {
        private readonly CourierAppDbContext _dbContext;

        public GeolocationService(CourierAppDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task GetLocation(GeolocationViewModel model)
        {
            throw new NotImplementedException();
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
