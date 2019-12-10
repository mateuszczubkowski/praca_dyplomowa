﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Courier;
using CourierApp.Data;
using CourierApp.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CourierApp.Core.Implementation
{
    public class CourierManagementService : ICourierManagementService
    {
        private readonly CourierAppDbContext _dbContext;
        private readonly IReviewService _reviewService;

        public CourierManagementService(CourierAppDbContext dbContext, IReviewService reviewService)
        {
            _dbContext = dbContext;
            _reviewService = reviewService;
        }

        public async Task<int> AddCourier(CreateCourierViewModel model)
        {
            var courier = new Courier
            {
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = "niewazne"
            };

            await _dbContext.Couriers.AddAsync(courier);
            await _dbContext.SaveChangesAsync();

            return courier.Id;
        }

        public async Task<IEnumerable<CourierListItemViewModel>> GetCouriersList()
        {
            var couriers = await _dbContext.Couriers.AsNoTracking().Select(x => new CourierListItemViewModel()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                SecondName = x.SecondName,
                PhoneNumber =x.PhoneNumber,
                Id = x.Id
            }).ToListAsync();

            foreach (var courier in couriers)
            {
                courier.Mark = _reviewService.GetCourierAvgMark(courier.Id);
            }

            return couriers;
        }

        public async Task<CourierReviewsDetailsViewModel> GetCourier(int id)
        {
            return await _dbContext.Couriers.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new CourierReviewsDetailsViewModel()
                {
                    FirstName = x.FirstName,
                    SecondName = x.SecondName,
                    ReviewsList = _reviewService.GetCourierReviews(x.Id)
                }).FirstOrDefaultAsync();
        }

        public IEnumerable<SelectListItem> GetCouriers()
        {
            var couriers = _dbContext.Couriers.Select(x => new SelectListItem($"{x.FirstName} {x.SecondName}", x.Id.ToString()))
                .AsNoTracking().ToList();

            return couriers;
        }
    }
}
