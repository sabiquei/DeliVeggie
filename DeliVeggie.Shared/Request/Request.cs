using System;
namespace DeliVeggie.Shared.Request
{
    public class Request<T> : IRequest
    {
        public T Data { get; set; }
    }
}

