using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string imageUrl { get; set; }
        public Decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EventTypeID { get; set; }
        public int EventCategoryId { get; set; }
        public virtual EventType EventType { get; set; }     
        public virtual EventCategory EventCategory { get; set; }
       
    }
}
