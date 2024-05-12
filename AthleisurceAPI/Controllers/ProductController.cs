using AthleisurceAPI.Models;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AthleisurceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        MongoDBDataAccess db;
        private readonly IConfiguration _config;
        private readonly string tableName = "Athleisurce_Customers";

        public ProductController(IConfiguration config)
        {
            _config = config;
            db = new MongoDBDataAccess("MongoAthleisurceDB", _config.GetConnectionString("Default"));
        }
        // GET: api/<ProductController>
        [HttpGet("/api/GetCustsomerById/{id}")]
        public CustomerModel GetCustomerCart(string id)
        {
            Console.Write($"{id}");
            Guid guid = new Guid(id);
            return db.LoadRecordById<CustomerModel>(tableName, guid);
        }

        // GET All Customers
        [HttpGet("/api/GetAllCustomers")]
        public List<CustomerModel> GetCustomer()
        {
            return db.LoadRecords<CustomerModel>(tableName);
        }

        // Get Cart
        [HttpGet("/api/GetCart/{id}")]
        public List<ProductModel> GetCart(string id)
        {
            Guid guid = new Guid(id);
            var customerInfo = db.LoadRecordById<CustomerModel>(tableName, guid);
            var cart = customerInfo.Cart;
            return cart;
        }

        // Get Orders
        [HttpGet("/api/GetOrders/{id}")]
        public List<OrderModel> GetOrder(string id)
        {
            Guid guid = new Guid(id);
            var customerInfo = db.LoadRecordById<CustomerModel>(tableName, guid);
            var orders = customerInfo.Orders;
            return orders;
        }

        // POST api/<ProductController>
        [HttpPost("/api/RegisterCustomer")]
        public void InsertRegisteredCustomer(CustomerModel customer)
        {
            db.UpsertRecord<CustomerModel>(tableName, customer.Id, customer);
        }


        [HttpPatch("/api/AddToCart/{id}")]
        public void AddToCart(string id, ProductModel value)
        {
            Guid guid = new Guid(id);
            var updateDefinition = Builders<CustomerModel>.Update.Push(entity => entity.Cart, value);
            db.UpdateRecord(tableName, guid, updateDefinition);
        }

        [HttpPatch("/api/RemoveFromCart/{id}")]
        public void RemoveFromCart(string id, ProductModel value) 
        {
            Guid guid = new Guid(id);
            var updateDefinition = Builders<CustomerModel>.Update.Pull(entity => entity.Cart, value);
            db.UpdateRecord(tableName, guid, updateDefinition);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Console.Write($"deleted {id}");
        }
    }
}
