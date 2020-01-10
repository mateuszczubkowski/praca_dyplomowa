using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels.Courier;
using CourierApp.MailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourierApp.WebApp.Controllers
{
    [Authorize]
    public class CourierController : Controller
    {
        private readonly ICourierManagementService _courierManagement;
        private readonly IEmailService _emailService;

        public CourierController(ICourierManagementService courierManagement, IEmailService emailService)
        {
            _courierManagement = courierManagement;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CourierList()
        {
            var result = await _courierManagement.GetCouriersList();

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourierViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _courierManagement.AddCourier(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> SendEmail()
        {
            await _emailService.SendEmailAsync("mateusz.czubkowski@gmail.com", "test", "test");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public void PostLocation([FromBody]double lat/*, [FromBody]double lng*/)
        {
            var x = lat;

            //var y = lng;
        }

    }
}