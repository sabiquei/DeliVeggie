using System;
using DeliVeggie.Shared.Dto;
using DeliVeggie.Shared.Request;
using DeliVeggie.Shared.Response;

namespace DeliVeggie.MicroService.Abstract
{
	public interface IProductService
	{
        IResponse HandleRequest(IRequest arg);
        double CalculatePriceWithReduction(ProductDto product, int dayOfWeek);
    }
}

