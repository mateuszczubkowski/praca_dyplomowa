using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourierApp.WebApp.Dto.Courier
{
    public class GetCourierListDto
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public decimal Mark { get; set; }
    }
}
