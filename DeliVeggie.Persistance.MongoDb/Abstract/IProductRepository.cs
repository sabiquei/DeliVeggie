using System;
using DeliVeggie.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliVeggie.Persistance.MongoDb.Abstract
{
	public interface IProductRepository
	{
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(string id);
    }
}

