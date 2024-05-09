using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLibrary.Models
{
    public class ProductModel
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quanity { get; set; }

        public string Size { get; set; }

        public string ImageURL { get; set; }

        public string Category { get; set; }
    }
}
