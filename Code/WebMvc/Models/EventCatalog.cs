using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    // gets a list of each event already mapped to corresponding WebMVC model
    //and presents on to UI.
    //could we make this a generic, so that all types of objects like  
    //event categories and event types can use it rendered on screen
    //should we have a vm that do that instead?

    public class EventCatalog
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public List<Event> Data { get; set; }
        
    }
}
