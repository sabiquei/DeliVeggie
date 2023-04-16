using System;
using DeliVeggie.Shared.Request;
using DeliVeggie.Shared.Response;

namespace DeliVeggie.Shared.MessagingQueue.Abstract
{
    public interface ISubscriber
    {
        void Subscribe(Func<IRequest, IResponse> data);
    }
}

