using System;
using MongoDB.Driver;

namespace DeliVeggie.Persistance.MongoDb.DBContext.Abstract
{
	public interface IMongoDbContext
	{
		IMongoCollection<T> GetCollection<T>(string name);
	}
}

