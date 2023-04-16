using System;
using DeliVeggie.Persistance.MongoDb.DBContext.Abstract;
using DeliVeggie.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DeliVeggie.Persistance.MongoDb.DBContext
{
	public class MongoDbContext: IMongoDbContext
	{
        private IMongoDatabase _db { get; set; }
        private IMongoClient _client { get; set; }

        public MongoDbContext(IOptions<MongoSettings> config)
        {
            _client = new MongoClient(config.Value.ConnectionString);
            _db = _client.GetDatabase(config.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            if(string.IsNullOrWhiteSpace(collectionName))
            {
                return null;
            }
            return _db.GetCollection<T>(collectionName);
        }
    }
}

