using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models.Orders
{
    public class OrderTicket
    {
        public int OrderId { get; set; }

        public int TicketTypeId { get; set; }

        public string TypeName { get; set; }

        public int Quantity { get; set; } 
    }
}
