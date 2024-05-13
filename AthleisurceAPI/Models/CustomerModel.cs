using DataAccessLibrary.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace AthleisurceAPI.Models
{
    public class CustomerModel
    {
        [BsonId]
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public ShippingAddressModel Address { get; set; } = new ShippingAddressModel();
    }
}
