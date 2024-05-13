using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace DataAccessLibrary
{
    public class MongoDBDataAccess
    {

        private IMongoDatabase db;

        public MongoDBDataAccess(string dbName, string connectionString)
        {
            var client = new MongoClient(connectionString);
            db = client.GetDatabase(dbName);
        }

        public void InsertRecord<T>(string tableName, T record)
        {
            var collection = db.GetCollection<T>(tableName);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string tableName)
        {
            var collection = db.GetCollection<T>(tableName);
            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string tableName, Guid id)
        {
            var collection = db.GetCollection<T>(tableName);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        public void UpsertRecord<T>(string tableName, Guid id, T record)
        {
            var collection = db.GetCollection<T>(tableName);
            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new UpdateOptions { IsUpsert = true });
        }

        public void DeleteRecord<T>(string tableName, Guid id)
        {
            var collection = db.GetCollection<T>(tableName);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }

        public void UpdateRecord<T>(string tableName, Guid id, UpdateDefinition<T> updateDefinition)
        {
            var collection = db.GetCollection<T>(tableName);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.UpdateOne(filter, updateDefinition);
        }
    }
}
