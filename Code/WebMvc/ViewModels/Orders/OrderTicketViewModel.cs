using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models.Order;


namespace WebMvc.ViewModels.Orders
{
    public class OrderTicketViewModel
    {
        IEnumerable<WebMvc.Models.Order.Order> _allOrders;
        IEnumerable<WebMvc.Models.Order.Order> _upcomingOrders;
        IEnumerable<WebMvc.Models.Order.Order> _pastOrders;
        IEnumerable<WebMvc.Models.Order.Order> _canceledOrders;

        public IEnumerable<WebMvc.Models.Order.Order> Orders { get; set; }
        public IEnumerable<OrderTicket> OrdersTickets { get; set; }

        public IEnumerable<WebMvc.Models.Order.Order> UpcomingOrders() {
            if (_upcomingOrders == null) {
                _upcomingOrders= Orders.Where(c=> c.EventStartDate > DateTime.Today);
            }
            return _upcomingOrders;
        }

        public IEnumerable<WebMvc.Models.Order.Order> PastOrders()
        {
            if (_pastOrders == null)
            {
                _pastOrders=Orders.Where(c => c.EventStartDate < DateTime.Today);
            }
            return _pastOrders;
        }

        public IEnumerable<WebMvc.Models.Order.Order> CanceledOrders()
        {
            if (_canceledOrders == null)
            {
                _canceledOrders = Orders.Where(c => c.OrderStatus == OrderStatus.Canceled);
            }
            return _canceledOrders;
        }

      
    }
}
