using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models.Orders;

namespace WebMvc.Services.Orders
{
    public class MockOrderService : IOrderService
    {



        private List<Order> MockOrders = new List<Order>()
        {
            new Order { OrderId = 1 ,OrderDate = DateTime.Now, OrderStatus = OrderStatus.Delivered, BuyerId = "1" ,UserName="wen@gmail.com", FirstName="Wendy", LastName="Doe", Address="1234 Warren Av North, Bellevue, WA",EventId=1 ,EventTitle = "A Night with Katy Perry", EventStartDate = new DateTime(2018, 8, 10, 7, 10, 0), EventEndDate = new DateTime(2018, 10, 10, 9, 15, 0), PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1",OrderTotal=100 },
            new Order { OrderId = 2 ,OrderDate = DateTime.Now, OrderStatus = OrderStatus.Delivered, BuyerId = "1" ,UserName="wen@gmail.com", FirstName="Wendy", LastName="Doe", Address="1234 Warren Av North, Bellevue, WA",EventId=1 ,EventTitle = "A Night with Katy Perry", EventStartDate = new DateTime(2018, 4, 16, 20, 30, 0), EventEndDate = new DateTime(2018, 4, 16, 23, 0, 0), PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2", OrderTotal=25},
        };

        private List<OrderTicket> MockTickets = new List<OrderTicket>() {

            new OrderTicket{ OrderId=1, TicketTypeId=2,TypeName="VIP", Quantity=2, Price=50 },
            new OrderTicket{ OrderId=2, TicketTypeId=2,TypeName="VIP", Quantity=1, Price=25 },
            new OrderTicket{ OrderId=2, TicketTypeId=1,TypeName="Free", Quantity=1, Price=0 },
            };


        public async Task<Order> GetOrderByIdAsync(int orderId )
        {
            await Task.Delay(10);
            //  var token = await GetUserTokenAsync();
            string token ="Temp"; // I dont wan't to test tokerService

            if (!string.IsNullOrEmpty(token))
            {

              return  MockOrders.FirstOrDefault(o => o.OrderId == orderId);                                            
            }
            else
                return new Order();
        }

     
        public async Task<List<Order>> GetOrdersByBuyerAsync(string buyerId, int page, int take)
        {
            await Task.Delay(10);
            //  var token = await GetUserTokenAsync();
            string token = "Temp"; // I dont wan't to test tokerService

            if (!string.IsNullOrEmpty(token))
            {

                return MockOrders.Where(o => o.BuyerId == buyerId).ToList();
            }
            else
                return new List<Order>(); 
        }


    }

       
}
