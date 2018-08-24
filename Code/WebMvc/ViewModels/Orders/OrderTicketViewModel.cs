using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models.Orders;

namespace WebMvc.ViewModels.Orders
{
    public class OrderTicketViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderTicket> OrdersTickets { get; set; }


        public int CountTickets() {
           return  OrdersTickets.Sum(o => o.Quantity);
        }
    }
}
