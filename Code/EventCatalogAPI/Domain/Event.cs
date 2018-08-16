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
        public virtual EventType EventType { get; set; }     
        public virtual EventCategory EventCategory { get; set; }
        public int OrganizerId { get; internal set; }
        public string OrganizerName { get; set; }
        public string OrganizerDescription { get; set; }
        public string EventDescription { get; set; }
    }
}
