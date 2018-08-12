using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    // gets a list of each event/eventcategory/eventype already mapped to corresponding WebMVC model
    //and presents on to UI.

    public class EventTypeCatalog
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        // public List<Event> Data { get; set; }
        //did this generic  because we could have events, event categories and event types rendered on screen
        //should we have a vm that does this instead?
        public IEnumerable<EventType> Data { get; set; } 
    }
}
