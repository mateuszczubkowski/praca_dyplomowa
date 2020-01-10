using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourierApp.WebApp.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Create(string link)
        {
            var model = new CreateReviewViewModel {Link = link};

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewViewModel model)
        {
            await _reviewService.Create(model);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ReviewList(int id)
        {
            var result = await _reviewService.GetCourierReviews(id);

            return View(result);
        }
    }
}