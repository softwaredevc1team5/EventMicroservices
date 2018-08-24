using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Services.Orders;
using WebMvc.ViewModels.Orders;

namespace WebMvc.Controllers
{
    public class OrderTicketController:Controller
    {

        private IOrderService _orderService;

        //Constructor receiving the service that this controller is going to use to get the data.
        public OrderTicketController(IOrderService orderSvc) {
            _orderService = orderSvc;
        }


        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetOrdersByBuyerAsync("1",1,1);
            var orderTickets = await _orderService.GetOrderTicketByOrderIdAsync(2,1,1);

            var vm = new OrderTicketViewModel
            {
                Orders = orders
              //  OrdersTickets = orderTickets
            };

            return View(vm);
        }
    }
}
