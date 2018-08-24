using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models.Orders;

namespace WebMvc.Services.Orders
{
    interface IOrderService
    {
        Task<Order> GetOrdersByBuyer(int buyerId, int page, int take);
        Task<Order> GetOrderById(int orderId);
    }
}
