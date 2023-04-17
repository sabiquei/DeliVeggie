using System;
using DeliVeggie.Shared.Dto;
using DeliVeggie.Shared.Request;
using DeliVeggie.Shared.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliVeggie.Persistance.MongoDb;
using System.Linq;
using DeliVeggie.MicroService.Abstract;
using DeliVeggie.Persistance.MongoDb.Abstract;

namespace DeliVeggie.MicroService
{
	public class ProductService: IProductService
	{
        private static IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
		{
            _productRepository = productRepository;
		}

        public IResponse HandleRequest(IRequest arg)
        {
            if (arg is Request<ProductDetailsRequest> detailsRequest)
            {
                Console.WriteLine($"Gateway sent a request to retrieve details of product with ID {detailsRequest.Data.ProductId}.");
                var details = Task.Run(async () =>
                {
                    return await _productRepository.GetByIdAsync(detailsRequest.Data.ProductId);
                }).GetAwaiter().GetResult();

                if (details == null)
                {
                    return new Response<ProductDetailsResponse>();
                }

                return new Response<ProductDetailsResponse>()
                {
                    Data = new ProductDetailsResponse()
                    {
                        Id = details.Id,
                        Name = details.Name,
                        EntryDate = details.EntryDate,
                        PriceWithReduction = CalculatePriceWithReduction(details, ((int)DateTime.Now.DayOfWeek) + 1)
                    }
                };
            }
            else
            {
                Console.WriteLine($"Gateway sent a request to retrieve all products.");

                var details = Task.Run(async () =>
                {
                    return await _productRepository.GetAllAsync();
                }).GetAwaiter().GetResult();

                if (details == null)
                {
                    return new Response<List<ProductDetailsResponse>>();
                }

                return new Response<List<ProductDetailsResponse>>()
                {
                    Data = details.Select(x => new ProductDetailsResponse()
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList()
                };
            }
        }

        public double CalculatePriceWithReduction(ProductDto product, int dayOfWeek)
        {
            var priceReduction = product?.PriceReductions?.FirstOrDefault(x => x.DayOfWeek == dayOfWeek);

            if (priceReduction == null)
            {
                return product.Price;
            }

            return product.Price * (1 - priceReduction.Reduction);
        }
    }
}

