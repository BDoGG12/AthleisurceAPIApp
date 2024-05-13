using DataAccessLibrary.Models;

namespace AthleisurceAPI.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public List<ProductModel> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public CustomerModel Customer { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
