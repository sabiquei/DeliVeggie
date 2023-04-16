using System;
namespace DeliVeggie.Shared.Response
{
    public class Response<T> : IResponse
    {
        public T Data { get; set; }
    }
}

