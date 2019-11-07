using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Review;
using CourierApp.Data;
using CourierApp.Data.Models;
using CourierApp.MailService;
using Microsoft.EntityFrameworkCore;

namespace CourierApp.Core.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly CourierAppDbContext _dbContext;
        private readonly IEmailService _mailService;

        public ReviewService(CourierAppDbContext dbContext, IEmailService mailService)
        {
            _dbContext = dbContext;
            _mailService = mailService;
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

        public string CreateReviewLink(int courierId)
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            return $"{unixTimestamp.ToString()}{courierId.ToString()}";
        }

        public async Task CreateReviewLink(int courierId, string reviewLink)
        {
            var newReviewLink = new ReviewLink
            {
                CourierId = courierId,
                Link = reviewLink
            };

            await _dbContext.ReviewLinks.AddAsync(newReviewLink);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Create(string content, int mark, string author, string link)
        {
            var reviewLink = await _dbContext.ReviewLinks.FirstOrDefaultAsync(rl => rl.Link == link);

            if (reviewLink != null)
            {
                _dbContext.ReviewLinks.Remove(reviewLink);
                await _dbContext.SaveChangesAsync();

                var review = new Review
                {
                    Author = author,
                    Content = content,
                    CourierId = Convert.ToInt32(link.Substring(link.Length - 2)),
                    Mark = mark
                };

                await _dbContext.Reviews.AddAsync(review);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                //error
            }

        }

        public async Task SendReviewLink(string mailTo, string link)
        {
            await _mailService.SendEmailAsync(mailTo, "Wystaw opinię", link);
        }
    }
}
