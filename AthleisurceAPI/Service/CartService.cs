using AthleisurceAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace AthleisurceAPI.Service
{
    public class CartService
    {
        private readonly IMongoCollection<CartItemModel> _cartCollection;
        private readonly IConfiguration _config;

        public CartService(string databaseName, string collectionName)
        {
            var connectionString = _config.GetConnectionString("ConnectionStrings");
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase(databaseName);
            _cartCollection = database.GetCollection<CartItemModel>(collectionName);
        }

        public List<CartItemModel> GetCartItems(string customerId)
        {
            Guid guid = new Guid(customerId);
            return _cartCollection.Find(item => item.CustomerInfo.Id == guid).ToList();
        }

        public void AddToCart(CartItemModel item)
        {
            _cartCollection.InsertOne(item);
        }

        public void UpdateCartItem(string customerId, string productId, int newQuantity)
        {
            var customerGuid = new Guid(customerId);
            var productGuid = new Guid(productId);
            var filter = Builders<CartItemModel>.Filter.Eq(item => item.CustomerInfo.Id, customerGuid) &
                         Builders<CartItemModel>.Filter.Eq(item => item.ProductId, productGuid);

            var update = Builders<CartItemModel>.Update.Set(item => item.Quantity, newQuantity);

            _cartCollection.UpdateOne(filter, update);
        }

        public void RemoveFromCart(string customerId, string productId)
        {
            var customerGuid = new Guid(customerId);
            var productGuid = new Guid(productId);

            var filter = Builders<CartItemModel>.Filter.Eq(item => item.CustomerInfo.Id, customerGuid) &
                         Builders<CartItemModel>.Filter.Eq(item => item.ProductId, productGuid);
            _cartCollection.DeleteOne(filter);
        }
    }
}
