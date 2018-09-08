using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Messaging
{
    public class OrderCompletedEvent
    {
        public string BuyerId { get; set; }
        public int EventId { get; set; }
        //public String RegMessage { get; set; }
        public OrderCompletedEvent(int eventId, string buyerId)
        {
            BuyerId = buyerId;
            EventId = eventId;
            //RegMessage = "This message has been registered";
        }

    }
}
