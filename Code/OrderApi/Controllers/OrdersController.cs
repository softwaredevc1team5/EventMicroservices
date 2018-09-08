using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Messaging;
using EventMicroservices.Services.OrderApi.Data;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrderApi.Models;

namespace OrderApi.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]

    public class OrdersController : Controller
    {

        private readonly OrdersContext _ordersContext;
        private readonly IOptionsSnapshot<OrderSettings> _settings;


        private readonly ILogger<OrdersController> _logger;
        private IBus _bus;

        public OrdersController(OrdersContext ordersContext, ILogger<OrdersController> logger, IOptionsSnapshot<OrderSettings> settings, IBus bus)
        {
            _settings = settings;
            // _ordersContext = ordersContext;
            _ordersContext = ordersContext ?? throw new ArgumentNullException(nameof(ordersContext));

            ((DbContext)ordersContext).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            _bus = bus;
            _logger = logger;
        }



        // POST api/Order
        [Route("new")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOrder(
            [FromBody] Order order)
        {
            //var envs = Environment.GetEnvironmentVariables();
            //var conString = _settings.Value.ConnectionString;
            //_logger.LogInformation($"{conString}");

            //order.OrderStatus = OrderStatus.Preparing;
            order.OrderDate = DateTime.UtcNow;

            //_logger.LogInformation(" In Create Order");
            //_logger.LogInformation(" Order" +order.UserName);


            _ordersContext.Orders.Add(order);
            _ordersContext.OrderTickets.AddRange(order.OrderTicket);

            //_logger.LogInformation(" Order added to context");
            //_logger.LogInformation(" Saving........");

            await _ordersContext.SaveChangesAsync();
            //_bus.Publish(new OrderCompletedEvent(order.EventId, order.BuyerId))
              //  .Wait();
            _bus.Publish(new OrderCompletedEvent(5, order.BuyerId))
                .Wait();
            return CreatedAtRoute("GetOrder", new { id = order.OrderId }, order);

        }

        [HttpGet("{id}", Name = "GetOrder")]
        //  [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrder(int id)
        {

            var item = await _ordersContext.Orders
                .Include(x => x.OrderTicket)
                .SingleOrDefaultAsync(ci => ci.OrderId == id);
            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();

        }



        [Route("")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _ordersContext.Orders.ToListAsync();


            return Ok(orders);
        }


        #region Display Orders
        [HttpGet]
        //   [HttpGet("{buyerid}", Name = "GetOrderByBuyerId")]
        [Route("byBuyerId/{buyerid}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOrderByUserId(string buyerid)
        {
            var root = (IQueryable<Order>)_ordersContext.Orders;         
            var itemsOnPage = await root.Where(ci => ci.BuyerId == buyerid).ToListAsync();
           

            if (itemsOnPage != null)
            {
                return Ok(itemsOnPage);
            }

            return NotFound();

        }

        #endregion


    }
}
