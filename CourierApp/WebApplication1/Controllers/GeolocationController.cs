using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourierApp.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourierApp.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeolocationController : Controller
    {
        [HttpPost]
        [Route("Location")]
        public async Task PostLocation([FromBody] GeolocationViewModel position)
        {
            var x = position.Latitude;

            var y = position.Longitude;
        }
    }
}