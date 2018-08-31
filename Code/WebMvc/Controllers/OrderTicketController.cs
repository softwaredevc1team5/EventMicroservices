using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;
using WebMvc.Services;

using WebMvc.ViewModels.Orders;

namespace WebMvc.Controllers
{
    public class OrderTicketController:Controller
    {

        private IOrderService _orderService;
        private readonly IIdentityService<ApplicationUser> _identityService;

        //Constructor receiving the service that this controller is going to use to get the data.
        public OrderTicketController(IOrderService orderSvc, IIdentityService<ApplicationUser> identityService) {
            _orderService = orderSvc;
            _identityService = identityService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            var user = _identityService.Get(HttpContext.User);
            //  var orders = await _orderService.GetOrdersByBuyerAsync(user.Id,1,1); When Lisa finish we have uncomment this line
            var orders = await _orderService.GetOrdersByUserNameAsync(user.Email,1,1);
            var vm = new OrderTicketViewModel
            {
                Orders = orders
              
            };

            return View(vm);
        }
    }
}
