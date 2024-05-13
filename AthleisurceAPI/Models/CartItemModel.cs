using DataAccessLibrary.Models;

namespace AthleisurceAPI.Models
{
    public class CartItemModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public CustomerModel CustomerInfo { get; set; }
        public ProductModel ProductInfo { get; set; }
    }
}
