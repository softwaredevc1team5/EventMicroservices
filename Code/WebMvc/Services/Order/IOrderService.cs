using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models.Orders;

namespace WebMvc.Services.Orders
{
    public interface IOrderService
    {
        //TO DO: We have to check if we should use Task<IEnumerable<SelectListItem>> intead of List<Order>.
         Task<Order> GetOrderByIdAsync(int orderId);
         Task<List<Order>> GetOrdersByBuyerAsync(string buyerId, int page, int take);
         Task<List<Order>> GetOrdersByUserNameAsync(string buyerId, int page, int take);
        
    }
}
