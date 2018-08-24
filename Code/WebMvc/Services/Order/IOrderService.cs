using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models.Orders;

namespace WebMvc.Services.Orders
{
    public interface IOrderService
    {
          Task<Order> GetOrderByIdAsync(int orderId);
          Task<List<Order>> GetOrdersByBuyerAsync(string buyerId, int page, int take);
    }
}
