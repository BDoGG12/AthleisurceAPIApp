using DataAccessLibrary.Models;

namespace AthleisurceAPI.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public List<ProductModel> Items { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
