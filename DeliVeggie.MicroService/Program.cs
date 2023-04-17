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
using DeliVeggie.MicroService.Abstract;
using DeliVeggie.Persistance.MongoDb.Abstract;

namespace DeliVeggie.MicroService
{
    public class Program
    {
        private static IConfiguration Configuration;

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
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<ISubscriber, Subscriber>();
            services.AddSingleton<IProductService, ProductService>();

            var serviceProvider = services.BuildServiceProvider();

            var productSerive = serviceProvider.GetService<IProductService>();
            var bus = serviceProvider.GetService<ISubscriber>();

            while(true)
            {
                bus?.Subscribe(productSerive.HandleRequest);
            }
        }
    }
}

