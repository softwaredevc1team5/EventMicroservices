using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class EventCreate
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Address { get; set; }
      //  [Required]
        public string City { get; set; }
     //   [Required]
        public string State { get; set; }
     //   [Required]
        public string Zipcode { get; set; }
        public string ImageUrl { get; set; }
        public Decimal Price { get; set; }
     //   [Required]
        public DateTime StartDate { get; set; }
     //   [Required]
        public DateTime EndDate { get; set; }
        public int EventTypeId { get; set; }
        public int EventCategoryId { get; set; }
      //  [Required]
        public string EventType { get; set; }
      //  [Required]
        public string EventCategory { get; set; }
        public int OrganizerId { get; internal set; }
      //  [Required]
        public string OrganizerName { get; set; }
      //  [Required]
        public string EventDescription { get; set; }
        public string OrganizerDescription { get; set; }
    }
}
