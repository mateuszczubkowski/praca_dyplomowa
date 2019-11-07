using System;
using System.Collections.Generic;
using System.Text;

namespace CourierApp.Data.Models
{
    public class ReviewLink
    {
        public int Id { get; set; }

        public string Link { get; set; }

        public int CourierId { get; set; }
    }
}
