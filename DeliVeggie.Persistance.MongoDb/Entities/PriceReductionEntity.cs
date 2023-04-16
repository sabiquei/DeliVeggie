using System;
namespace DeliVeggie.Persistance.MongoDb.Entities
{
	public class PriceReductionEntity
	{
		public int DayOfWeek { get; set; }
		public double Reduction { get; set; }
	}
}