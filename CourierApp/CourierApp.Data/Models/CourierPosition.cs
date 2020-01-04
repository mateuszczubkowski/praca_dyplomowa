using System;
using System.Collections.Generic;
using System.Text;

namespace CourierApp.Data.Models
{
    public class CourierPosition
    {
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int CourierId { get; set; }

        public DateTime Date { get; set; }
    }
}
