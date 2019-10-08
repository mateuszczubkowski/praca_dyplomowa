using System.Collections.Generic;

namespace CourierApp.Data.Models
{
    public class Courier
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<Package> Packages { get; set; }

        public IEnumerable<Review> Reviews { get; set; }
    }
}
