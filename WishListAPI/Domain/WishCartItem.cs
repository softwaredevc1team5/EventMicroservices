using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishListAPI.Domain
{
    public class WishCartItem
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public decimal TicketPrice { get; set; }
        public int NumOfTickets { get; set; }
        public string TicketType { get; set; }
    }
}
