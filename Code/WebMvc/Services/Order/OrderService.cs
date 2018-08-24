using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models.Orders;

namespace WebMvc.Services.Orders
{
    public class OrderService : IOrderService
    {
        public Task<Order> GetOrderById(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrdersByBuyer(int buyerId, int page, int take)
        {
            throw new NotImplementedException();
        }
    }
}
