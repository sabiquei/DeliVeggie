using System;
using DeliVeggie.Shared.MessagingQueue.Abstract;
using DeliVeggie.Shared.Request;
using DeliVeggie.Shared.Response;
using DeliVeggie.Shared.Settings;
using EasyNetQ;
using Microsoft.Extensions.Options;

namespace DeliVeggie.Shared.MessagingQueue
{
    public class Subscriber : ISubscriber
    {
        public readonly IBus _bus;

        public Subscriber(IOptions<MqSettings> options)
        {
            _bus = RabbitHutch.CreateBus(options.Value.ConnectionString);
        }
        public void Subscribe(Func<IRequest, IResponse> data)
        {
            _bus.Respond<IRequest, IResponse>(data);
        }
    }
}
