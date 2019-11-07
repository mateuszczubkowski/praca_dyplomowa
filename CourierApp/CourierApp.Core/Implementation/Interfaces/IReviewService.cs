using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CourierApp.Core.ViewModels.Review;

namespace CourierApp.Core.Implementation.Interfaces
{
    public interface IReviewService
    {
        decimal GetCourierAvgMark(int courierId);

        IEnumerable<ReviewListItemViewModel> GetCourierReviews(int id);

        string CreateReviewLink(int courierId);

        Task CreateReviewLink(int courierId, string reviewLink);

        Task Create(CreateReviewViewModel model);

        Task SendReviewLink(string mailTo, string link);
    }
}
