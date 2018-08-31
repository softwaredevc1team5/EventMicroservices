using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models.CartModels
{
    public class CartItem
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string TicketType { get; set; }
        public int TicketTypeId { get; set; }
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string ImageUrl { get; set; }
        public Decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime EndDate { get; set; }
        public int EventTypeId { get; set; }
        public int EventCategoryId { get; set; }
        public string EventType { get; set; }
        public string EventCategory { get; set; }
        public int OrganizerId { get; internal set; }
        public string OrganizerName { get; set; }
        public string EventDescription { get; set; }
    }
}
