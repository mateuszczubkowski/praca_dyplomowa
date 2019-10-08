namespace CourierApp.Data.Models
{
    public class Package
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string CustomerEmail { get; set; }

        public string Status { get; set; }

        public int CourierId { get; set; }

        public Courier Courier { get; set; }
    }
}
