using System;
using DeliVeggie.Shared.MessagingQueue.Abstract;
using DeliVeggie.Shared.Request;
using DeliVeggie.Shared.Response;
using DeliVeggie.Shared.Settings;
using EasyNetQ;
using Microsoft.Extensions.Options;

namespace DeliVeggie.Shared.MessagingQueue
{
    public class Publisher : IPublisher
    {
        public readonly IBus _bus;

        public Publisher(IOptions<MqSettings> options)
        {
            _bus = RabbitHutch.CreateBus(options.Value.ConnectionString);
        }

        public IResponse Request(IRequest request)
        {
            return _bus.Request<IRequest, IResponse>(request);
        }
    }
}

