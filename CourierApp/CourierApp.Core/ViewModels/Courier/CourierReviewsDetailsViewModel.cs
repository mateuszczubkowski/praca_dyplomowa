using System;
using System.Collections.Generic;
using System.Text;
using CourierApp.Core.ViewModels.Review;

namespace CourierApp.Core.ViewModels.Courier
{
    public class CourierReviewsDetailsViewModel
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public IEnumerable<ReviewListItemViewModel> ReviewsList { get; set; }
    }
}
