namespace AthleisurceAPI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quanity { get; set; }

        public string Size { get; set; }

        public byte[] ImageData { get; set; }

        public string Category { get; set; }
    }
}
