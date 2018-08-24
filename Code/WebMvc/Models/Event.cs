using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    //mirror Event model/table in EventCatalog API
    //so basically when httpclient makes request (like HTTPGET) to get all events from events table
    //instead of returning a json of an event, it maps the data for an event from the events table  to an object of this class
    //so that it can be rendered on ui
    public class Event
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string ImageUrl { get; set; }
        public Decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EventTypeId { get; set; }
        public int EventCategoryId { get; set; }
        public string EventType { get; set; }
        public string EventCategory { get; set; }
        public int OrganizerId { get; internal set; }
        public string OrganizerName { get; set; }
        public string EventDescription { get; set; }
        public string OrganizerDescription { get; set; }
    }
}
