using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliVeggie.Persistance.MongoDb.DBContext.Abstract;
using DeliVeggie.Persistance.MongoDb.Entities;
using DeliVeggie.Shared.Dto;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeliVeggie.Persistance.MongoDb
{
    public class ProductRepository
    {
        private const string CollectionName = "Products";

        private readonly IMongoDbContext _dbContext;

        private IMongoCollection<ProductEntity> _products;

        public ProductRepository(IMongoDbContext dbContext)
        {
            _dbContext = dbContext;
            _products = dbContext.GetCollection<ProductEntity>(CollectionName);
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var result = await _products.FindAsync(p => true).Result.ToListAsync();

            return result?.Select(x => new ProductDto()
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                EntryDate = x.EntryDate,
                Price = x.Price
            })?.ToList();
        }

        public async Task<ProductDto> GetByIdAsync(string id)
        {
            if(string.IsNullOrWhiteSpace(id) || !ObjectId.TryParse(id, out ObjectId objectId))
            {
                return null;
            }

            var result = await _products.FindAsync(p => p.Id == objectId)?.Result?.FirstOrDefaultAsync();
            if(result == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = result.Id.ToString(),
                Name = result.Name,
                EntryDate = result.EntryDate,
                Price = result.Price,

                PriceReductions = result.PriceReductions?.Select(x => new PriceReductionDto()
                {
                    DayOfWeek = x.DayOfWeek,
                    Reduction = x.Reduction
                })?.ToList()
            };
        }
    }
}

