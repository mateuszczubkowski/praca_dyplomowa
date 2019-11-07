using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Review;
using Microsoft.AspNetCore.Mvc;

namespace CourierApp.WebApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public IActionResult Create(string link)
        {
            var model = new CreateReviewViewModel {Link = link};

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewViewModel model)
        {
            await _reviewService.Create(model);

            return RedirectToAction("Index", "Home");
        }
    }
}