using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class EventForCreation
    {

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public Decimal Price { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int EventTypeId { get; set; }
        [Required]
        public int EventCategoryId { get; set; }
        [Required]
        public int OrganizerId { get; internal set; }
        [Required]
        public string OrganizerName { get; set; }
        [Required]
        public string EventDescription { get; set; }
        [Required]
        public string OrganizerDescription { get; set; }

        public string EventType { get; set; }
        public string EventCategory { get; set; }
    }
}
