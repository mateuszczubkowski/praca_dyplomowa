using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp.Core.Implementation.Interfaces
{
    public interface IReviewService
    {
        Task<decimal> GetCourierAvgMark(int courierId);
    }
}
