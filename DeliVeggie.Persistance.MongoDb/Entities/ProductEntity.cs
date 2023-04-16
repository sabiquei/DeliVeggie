using System;
using System.Collections.Generic;

namespace DeliVeggie.Persistance.MongoDb.Entities
{
	public class ProductEntity: EntityBase
	{
		public string Name { get; set; }
		public DateTime EntryDate { get; set; }
		public double Price { get; set; }

		public IEnumerable<PriceReductionEntity> PriceReductions { get; set; }
    }
}

