using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourierApp.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeolocationController : Controller
    {
        private readonly IGeolocationService _gpsService;

        public GeolocationController(IGeolocationService gpsService)
        {
            _gpsService = gpsService;
        }


        [HttpPost]
        [Route("Location")]
        public async Task PostLocation(GeolocationViewModel position)
        {
            await _gpsService.CreateLocation(position);
        }
    }
}