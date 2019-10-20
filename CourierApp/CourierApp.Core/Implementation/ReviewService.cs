using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CourierApp.Core.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly CourierAppDbContext _dbContext;

        public ReviewService(CourierAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<decimal> GetCourierAvgMark(int courierId)
        {
            var reviews = await _dbContext.Reviews.AsNoTracking().Where(r => r.CourierId == courierId)
                .Select(r => r.Mark).ToListAsync();

            return Convert.ToDecimal(reviews.Average());
        }
    }
}
