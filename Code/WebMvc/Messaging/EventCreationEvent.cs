using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.Messaging
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
