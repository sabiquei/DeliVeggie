using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DeliVeggie.Shared.Settings;
using DeliVeggie.Persistance.MongoDb.DBContext;
using DeliVeggie.Persistance.MongoDb.DBContext.Abstract;
using DeliVeggie.Persistance.MongoDb;
using DeliVeggie.Shared.MessagingQueue;
using DeliVeggie.Shared.MessagingQueue.Abstract;
using DeliVeggie.Shared.Request;
using DeliVeggie.Shared.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DeliVeggie.Shared.Dto;

namespace DeliVeggie.MicroService
{
    public class Program
    {
        private static IConfiguration Configuration;

        private static ProductRepository _productRepository;

        static void Main(string[] args)
        {
            Console.WriteLine("Product Microservice Started..");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: false);
            Configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddLogging(configure => configure.AddConsole());

            services.Configure<MongoSettings>(Configuration.GetSection("MongoDb"));
            services.Configure<MqSettings>(Configuration.GetSection("MQ"));

            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            services.AddSingleton<ProductRepository>();
            services.AddSingleton<ISubscriber, Subscriber>();

            var serviceProvider = services.BuildServiceProvider();

            _productRepository = serviceProvider.GetService<ProductRepository>();
            var bus = serviceProvider.GetService<ISubscriber>();

            while(true)
            {
                bus?.Subscribe(HandleRequest);
            }
        }

        private static IResponse HandleRequest(IRequest arg)
        {
            if (arg is Request<ProductDetailsRequest> detailsRequest)
            {
                Console.WriteLine($"Gateway sent a request to retrieve details of product with ID {detailsRequest.Data.ProductId}.");
                var details = Task.Run(async () =>
                {
                    return await _productRepository.GetByIdAsync(detailsRequest.Data.ProductId);
                }).GetAwaiter().GetResult();

                if(details == null)
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
                        PriceWithReduction = CalculatePriceWithReduction(details)
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

        private static double CalculatePriceWithReduction(ProductDto product)
        {
            int dayOfWeek = ((int)DateTime.Now.DayOfWeek) + 1;
            var priceReduction = product?.PriceReductions?.FirstOrDefault(x => x.DayOfWeek == dayOfWeek);

            if(priceReduction == null)
            {
                return product.Price;
            }

            return product.Price * (1 - priceReduction.Reduction);
        }
    }
}

