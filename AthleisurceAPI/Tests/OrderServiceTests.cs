using AthleisurceAPI.Models;
using AthleisurceAPI.Service;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Xunit;

namespace AthleisurceAPI.Tests
{
    public class OrderServiceTests
    {
        private readonly OrderService _orderService;
        public OrderServiceTests()
        {
            // Set up MongoDB connection
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = "mongodb://localhost:27017/";
            var databaseName = "MongoAthleisurceDB";
            var collectionName = "Athleisurce_Order"; // Adjust collection name as needed

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<OrderModel>(collectionName);

            // Clear existing data in the collection
            collection.DeleteMany(_ => true);

            _orderService = new OrderService(databaseName, collectionName);
        }

        [Fact]
        public void CreateOrder_ShouldAddOrderToDatabase()
        {
            // Arrange
            var order = new OrderModel
            {
                Id = new Guid("customer1"),
                Subtotal = 30,
                OrderItems = new List<ProductModel>
                {
                    new ProductModel { Id = new Guid("product1"), Category = "shirts", Description = "cotton", Price = 15, Size = "small", Title = "Fun Shirt"},
                    new ProductModel { Id = new Guid("product2"), Category = "shirts", Description = "cotton", Price = 15, Size = "small", Title = "Fun Shirt 2"},
                },
                Customer = new CustomerModel 
                { 
                    Id = new Guid("customer1"), 
                    Address = new ShippingAddressModel
                    { State = "IL", City = "Vernon Hills", Country = "United States", PostalCode = "60061", StreetName = "200 My street ave"},
                    EmailAddress = "MyEmail@gmail.com",
                    FirstName = "Ben",
                    LastName = "Do",
                    Password = "Password",
                    PhoneNumber = "12345",
                },
                OrderDate = DateTime.Now
            };
        }

        [Fact]
        public void DeleteOrder_ShouldRemoveOrderFromDatabase()
        {
            // Arrange
            var order = new OrderModel
            {
                Id = new Guid("customer1"),
                OrderItems = new List<ProductModel>
                {
                    new ProductModel { Id = new Guid("product1"), Category = "shirts", Description = "cotton", Price = 15, Size = "small", Title = "Fun Shirt"},
                    new ProductModel { Id = new Guid("product2"), Category = "shirts", Description = "cotton", Price = 15, Size = "small", Title = "Fun Shirt 2"},
                },
                OrderDate = DateTime.Now
            };
            _orderService.CreateOrder(order);

            // Act
            _orderService.DeleteOrder(order.Id.ToString());

            // Assert
            var retrievedOrder = GetOrderFromDatabase(order.Id.ToString());
            Assert.Null(retrievedOrder);
        }

        private OrderModel GetOrderFromDatabase(string orderId)
        {
            // Set up MongoDB connection
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = "mongodb://localhost:27017/";
            var databaseName = "MongoAthleisurceDB";
            var collectionName = "Athleisurce_Order"; // Adjust collection name as needed

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            var collection = database.GetCollection<OrderModel>(collectionName);
            var guid = new Guid(orderId);
            return collection.Find(order => order.Id == guid).FirstOrDefault();
        }
    }
}
