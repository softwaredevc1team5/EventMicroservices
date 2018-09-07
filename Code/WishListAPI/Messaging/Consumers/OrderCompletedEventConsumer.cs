using Common.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishListAPI.Model;

namespace WishListApi.Messaging.Consumers
{
    public class OrderCompletedEventConsumer :IConsumer<OrderCompletedEvent>
    {
        private readonly IWishlistRepository _repository;
        private readonly ILogger<OrderCompletedEventConsumer> _logger;
        public OrderCompletedEventConsumer(IWishlistRepository repository, ILogger<OrderCompletedEventConsumer> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        public  Task Consume(ConsumeContext<OrderCompletedEvent> context)
        {
            _logger.LogWarning("We are in consume method now...");
            _logger.LogWarning("BuyerId:" +context.Message.EventId);

            // _repository.SetEventIdFromMessaging(context.Message.EventId, context.Message.BuyerId);
            _repository.SetEventIdFromMessaging(7, context.Message.BuyerId);
            return Task.FromResult(context.Message);
             
        }
        /******
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(ConsumeContext<OrderCompletedEvent> context)
        {
            _repository.SetEventIdFromMessaging(context.Message.EventId, context.Message.BuyerId);
            //throw new NotImplementedException();
        }********/
    }
}
