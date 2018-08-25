using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    // gets a list of each event/eventcategory/eventype already mapped to corresponding WebMVC model
    //and presents on to UI.

    public class EventCategoryCatalog
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public List<EventCategory> Data { get; set; } 
    }
}
