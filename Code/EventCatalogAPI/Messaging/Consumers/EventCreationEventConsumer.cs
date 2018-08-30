using Common.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using ShoesOnContainers.Services.CartApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Messaging.Consumers
{
    public class EventCreationEventConsumer
    {


      //  private readonly ICartRepository _repository;
        private readonly ILogger<EventCreationEventConsumer> _logger;
        public EventCreationEventConsumer(ILogger<EventCreationEventConsumer> logger)
        {
          //  _repository = repository;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<EventCreationEvent> context)
        {
            _logger.LogWarning("We are in consume method now...");
            _logger.LogWarning("BuyerId:" + context.Message.BuyerId);
           // return _repository.DeleteCartAsync(context.Message.BuyerId);

        }
    }
}
