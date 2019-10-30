using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Courier;
using CourierApp.Core.ViewModels.Review;
using CourierApp.Data;
using CourierApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourierApp.Core.Implementation
{
    public class CourierManagementService : ICourierManagementService
    {
        private readonly CourierAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IReviewService _reviewService;

        public CourierManagementService(CourierAppDbContext dbContext, IMapper mapper, IReviewService reviewService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _reviewService = reviewService;
        }

        public Task AddCourier()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CourierListItemViewModel>> GetCouriersList()
        {
            return await _dbContext.Couriers.AsNoTracking().Select(x => new CourierListItemViewModel()
            {
                Email = x.Email,
                FirstName = x.FirstName,
                SecondName = x.SecondName,
                PhoneNumber =x.PhoneNumber
            }).ToListAsync();

            //foreach (var courier in couriers)
            //{
            //    courier.Mark = _reviewService.GetCourierAvgMark(courier.)
            //}
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
    }
}
