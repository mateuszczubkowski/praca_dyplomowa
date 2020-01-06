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
        private readonly MailQueue _mailService;

        public ReviewService(CourierAppDbContext dbContext, MailQueue mailService)
        {
            _dbContext = dbContext;
            _mailService = mailService;
        }
        public decimal GetCourierAvgMark(int courierId)
        {
            var reviews = _dbContext.Reviews.AsNoTracking().Where(r => r.CourierId == courierId)
                .Select(r => r.Mark).ToList();

            return reviews.Count != 0 ? Convert.ToDecimal(reviews.Average()) : 0;
        }

        public async Task<IEnumerable<ReviewListItemViewModel>> GetCourierReviews(int id)
        {
            return await _dbContext.Reviews.AsNoTracking().Where(x => x.CourierId == id).Select(x =>
                new ReviewListItemViewModel()
                {
                    Author = x.Author,
                    Content = x.Content,
                    Mark = x.Mark
                }).ToListAsync();
        }

        public string CreateReviewLink(int? courierId)
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            return $"{unixTimestamp.ToString()}{courierId.ToString()}";
        }

        public async Task CreateReviewLink(int? courierId, string reviewLink, string email)
        {
            var newReviewLink = new ReviewLink
            {
                CourierId = Convert.ToInt32(courierId),
                Link = reviewLink,
                Author = email
            };

            await _dbContext.ReviewLinks.AddAsync(newReviewLink);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Create(CreateReviewViewModel model)
        {
            var reviewLink = await _dbContext.ReviewLinks.FirstOrDefaultAsync(rl => rl.Link == model.Link);

            if (reviewLink != null)
            {
                _dbContext.ReviewLinks.Remove(reviewLink);
                await _dbContext.SaveChangesAsync();

                var review = new Review
                {
                    Author = model.Author,
                    Content = model.Content,
                    CourierId = Convert.ToInt32(model.Link.Substring(model.Link.Length - 1)),
                    Mark = model.Mark
                };

                await _dbContext.Reviews.AddAsync(review);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                //error
            }

        }

        public void SendReviewLink(string mailTo, string link)
        {
            var mail = new MailDto()
            {
                Address = mailTo,
                Message = $"https://localhost:44380/review/create?link={link}",
                Subject = "Wystaw opinię"
            };

            _mailService.EnqueueMail(mail);
        }
    }
}
