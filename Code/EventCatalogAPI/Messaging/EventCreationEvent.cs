using EventCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Messaging
{
    public class EventCreationEvent
    {
        public Event e { get; set; }

        public EventCreationEvent(Event eve)
        {
            e = eve;
        }

    }
}
