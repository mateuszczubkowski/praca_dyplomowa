namespace CourierApp.Data.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Mark { get; set; } 

        public string Author { get; set; }

        public int CourierId { get; set; }

        public Courier Courier { get; set; }
    }
}
