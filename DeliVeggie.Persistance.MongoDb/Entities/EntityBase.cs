using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeliVeggie.Persistance.MongoDb.Entities
{
	public abstract class EntityBase
	{
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
    }
}

