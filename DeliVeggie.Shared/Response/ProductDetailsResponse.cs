using System;
namespace DeliVeggie.Shared.Response
{
	public class ProductDetailsResponse
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime EntryDate { get; set; }
        public double PriceWithReduction { get; set; }
    }
}

