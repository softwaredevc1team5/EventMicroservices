using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class EventDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string ImageUrl { get; set; }
        public Decimal Price { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int EventTypeId { get; set; }
        public int EventCategoryId { get; set; }
        public string EventType { get; set; }
        public string EventCategory { get; set; }
        public int OrganizerId { get; internal set; }
        public string OrganizerName { get; set; }

    }
}
