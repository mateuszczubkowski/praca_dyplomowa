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
    }
}
