using AthleisurceAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace AthleisurceAPI.Service
{
    public class OrderService
    {
        private readonly IMongoCollection<OrderModel> _orderCollection;
        private readonly IConfiguration _config;

        public OrderService(string databaseName, string collectionName)
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase(databaseName);
            _orderCollection = database.GetCollection<OrderModel>(collectionName);
        }

        public List<OrderModel> GetOrdersForCustomer(string customerId)
        {
            Guid guid = new Guid(customerId);
            return _orderCollection.Find(order => order.Customer.Id == guid).ToList();
        }

        public void CreateOrder(OrderModel order)
        {
            _orderCollection.InsertOne(order);
        }

        public void UpdateOrder(OrderModel updatedOrder)
        {
            var filter = Builders<OrderModel>.Filter.Eq(order => order.Id, updatedOrder.Id);
            var options = new ReplaceOptions { IsUpsert = true };
            _orderCollection.ReplaceOne(filter, updatedOrder, options);
        }

        public void DeleteOrder(string orderId)
        {
            Guid guid = new Guid(orderId);
            var filter = Builders<OrderModel>.Filter.Eq(order => order.Id, guid);
            _orderCollection.DeleteOne(filter);
        }

        public OrderModel GetOrderById(string orderId)
        {
            Guid guid = new Guid(orderId);
            return _orderCollection.Find(order => order.Id == guid).FirstOrDefault();
        }

    }
}
