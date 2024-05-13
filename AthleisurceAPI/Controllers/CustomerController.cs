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
    public class CustomerController : ControllerBase
    {

        MongoDBDataAccess db;
        private readonly IConfiguration _config;
        private readonly string tableName = "Athleisurce_Customers";

        public CustomerController(IConfiguration config)
        {
            _config = config;
            db = new MongoDBDataAccess("MongoAthleisurceDB", _config.GetConnectionString("Default"));
        }
        // GET: api/<CustomerController>
        [HttpGet("/api/GetCustsomerById/{id}")]
        public CustomerModel GetCustomerById(string id)
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


        // POST api/<CustomerController>
        [HttpPost("/api/RegisterCustomer")]
        public void InsertRegisteredCustomer(CustomerModel customer)
        {
            db.UpsertRecord<CustomerModel>(tableName, customer.Id, customer);
        }


        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            Guid guid = new Guid(id);
            db.DeleteRecord<CustomerModel>(tableName, guid);
        }
    }
}
