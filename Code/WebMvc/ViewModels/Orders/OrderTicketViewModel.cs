using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models.Orders;

namespace WebMvc.ViewModels.Orders
{
    public class OrderTicketViewModel
    {
        IEnumerable<Order> _allOrders;
        IEnumerable<Order> _upcomingOrders;
        IEnumerable<Order> _pastOrders;
        IEnumerable<Order> _canceledOrders;

        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<OrderTicket> OrdersTickets { get; set; }

        public IEnumerable<Order> UpcomingOrders() {
            if (_upcomingOrders == null) {
                _upcomingOrders= Orders.Where(c=> c.EventStartDate > DateTime.Today);
            }
            return _upcomingOrders;
        }

        public IEnumerable<Order> PastOrders()
        {
            if (_pastOrders == null)
            {
                _pastOrders=Orders.Where(c => c.EventStartDate < DateTime.Today);
            }
            return _pastOrders;
        }

        public IEnumerable<Order> CanceledOrders()
        {
            if (_canceledOrders == null)
            {
                _canceledOrders = Orders.Where(c => c.OrderStatus == OrderStatus.Canceled);
            }
            return _canceledOrders;
        }

      
    }
}
