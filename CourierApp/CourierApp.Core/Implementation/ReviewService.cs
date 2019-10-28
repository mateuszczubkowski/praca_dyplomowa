using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Review;
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
        public decimal GetCourierAvgMark(int courierId)
        {
            var reviews = _dbContext.Reviews.AsNoTracking().Where(r => r.CourierId == courierId)
                .Select(r => r.Mark).ToList();

            return Convert.ToDecimal(reviews.Average());
        }

        public IEnumerable<ReviewListItemViewModel> GetCourierReviews(int id)
        {
            return _dbContext.Reviews.AsNoTracking().Where(x => x.CourierId == id).Select(x =>
                new ReviewListItemViewModel()
                {
                    Author = x.Author,
                    Content = x.Content,
                    Mark = x.Mark
                }).ToList();
        }
    }
}
