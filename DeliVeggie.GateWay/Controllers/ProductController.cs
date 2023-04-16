using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliVeggie.Shared.MessagingQueue.Abstract;
using DeliVeggie.Shared.Request;
using DeliVeggie.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeliVeggie.GateWay.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IPublisher _publisher;

        public ProductsController(IPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var request = new Request<ProductsRequest>()
            {
                Data = new ProductsRequest()
            };
            var data = _publisher.Request(request);
            if (!(data is Response<List<ProductDetailsResponse>> response) || response.Data == null)
            {
                return NotFound();
            }
            return Ok(response.Data.Select(x => new
            {
                x.Id,
                x.Name
            }).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var request = new Request<ProductDetailsRequest>() { Data = new ProductDetailsRequest() { ProductId = id } };
            var data = _publisher.Request(request);
            if (!(data is Response<ProductDetailsResponse> response) || response.Data == null)
            {
                return NotFound();
            }
            return Ok(response.Data);
        }
    }
}

