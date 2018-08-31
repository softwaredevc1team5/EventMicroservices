using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishListAPI.Model
{
    public class WishlistItems
    {
        public string Id { get; set; }
        public string productId { get; set; }
        public string EventTitle { get; set; }
        public decimal TicketPrice { get; set; }
        public decimal OldTicketPrice { get; set; }
        public string  City{ get; set; }
        public string TicketType { get; set; }
        public string ImageUrl { get; set; }
    }
}
