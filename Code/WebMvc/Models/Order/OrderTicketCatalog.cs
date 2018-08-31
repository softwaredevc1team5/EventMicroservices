using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models.Order
{
    public class OrderTicketCatalog
    {

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public List<OrderTicket> Data { get; set; }
    }
}

