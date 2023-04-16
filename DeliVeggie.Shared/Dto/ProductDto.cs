using System;
using System.Collections.Generic;

namespace DeliVeggie.Shared.Dto
{
	public class ProductDto
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime EntryDate { get; set; }
        public double Price { get; set; }

        public List<PriceReductionDto> PriceReductions { get; set; }
    }

    public class PriceReductionDto
    {
        public int DayOfWeek { get; set; }
        public double Reduction { get; set; }
    }
}

