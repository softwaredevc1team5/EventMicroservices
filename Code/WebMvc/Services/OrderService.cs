﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMvc.Infrastructure;
using WebMvc.Models;
using WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvc.Models.Order;
using System.Linq;

namespace WebMvc.Services
{
    public class OrderService : IOrderService
    {
        private IHttpClient _apiClient;
        private readonly string _remoteServiceBaseUrl;
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly ILogger _logger;

        public OrderService(IOptionsSnapshot<AppSettings> settings, IHttpContextAccessor httpContextAccesor, IHttpClient httpClient, ILoggerFactory logger)
        {
            _remoteServiceBaseUrl = $"{settings.Value.OrderUrl}/api/v1/orders";
            _settings = settings;
            _httpContextAccesor = httpContextAccesor;
            _apiClient = httpClient;
            _logger = logger.CreateLogger<OrderService>();
        }

        async public Task<Order> GetOrder(string id)
        {
            var token = await GetUserTokenAsync();
            var getOrderUri = ApiPaths.Order.GetOrder(_remoteServiceBaseUrl, id);

            var dataString = await _apiClient.GetStringAsync(getOrderUri, token);
            _logger.LogInformation("DataString: " + dataString);
            var response = JsonConvert.DeserializeObject<Order>(dataString);

            return response;
        }

        public async Task<List<Order>> GetOrders()
        {
            var token = await GetUserTokenAsync();
            var allOrdersUri = ApiPaths.Order.GetOrders(_remoteServiceBaseUrl);

            var dataString = await _apiClient.GetStringAsync(allOrdersUri, token);
            var response = JsonConvert.DeserializeObject<List<Order>>(dataString);

            return response;
        }


        public async Task<int> CreateOrder(Order order)
        {
            var token = await GetUserTokenAsync();

            var addNewOrderUri = ApiPaths.Order.AddNewOrder(_remoteServiceBaseUrl);
            _logger.LogDebug(" OrderUri " + addNewOrderUri);


            var response = await _apiClient.PostAsync(addNewOrderUri, order, token);
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error creating order, try later.");
            }

            // response.EnsureSuccessStatusCode();
            var jsonString = response.Content.ReadAsStringAsync();

            jsonString.Wait();
            _logger.LogDebug("response " + jsonString);
            dynamic data = JObject.Parse(jsonString.Result);
            string value = data.orderId;
            return Convert.ToInt32(value);
        }

        async Task<string> GetUserTokenAsync()
        {
            var context = _httpContextAccesor.HttpContext;

            return await context.GetTokenAsync("access_token");
        }




        //public async  Task<List<Order>> GetOrdersByUser(ApplicationUser user)
        //{
        //    var token = await GetUserTokenAsync();
        //    var allMyOrdersUri = ApiPaths.Order.GetOrdersByUser(_remoteServiceBaseUrl,user.Email);

        //    var dataString = await _apiClient.GetStringAsync(allMyOrdersUri, token);
        //    var response = JsonConvert.DeserializeObject<List<Order>>(dataString);

        //    return response;
        //}

        #region For Displaying Orders
        private List<Order> MockOrders = new List<Order>()
        {
            new Order { OrderId = 1 ,OrderDate = DateTime.Now, OrderStatus = OrderStatus.Delivered, BuyerId = "1" ,UserName="wen@gmail.com", FirstName="Wendy", LastName="Doe", Address="1234 Warren Av North, Bellevue, WA",EventId=1 ,EventTitle = "A Night with Katy Perry", EventStartDate = new DateTime(2018, 8, 10, 7, 10, 0), EventEndDate = new DateTime(2018, 8, 10, 9, 15, 0), PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1",OrderTotal=100, NumTotalTickets=2},
            new Order { OrderId = 2 ,OrderDate = DateTime.Now, OrderStatus = OrderStatus.Delivered, BuyerId = "1" ,UserName="wen@gmail.com", FirstName="Wendy", LastName="Doe", Address="1234 Warren Av North, Bellevue, WA",EventId=1 ,EventTitle = "A Night with Katy Perry", EventStartDate = new DateTime(2018, 10, 16, 20, 30, 0), EventEndDate = new DateTime(2018, 10, 16, 23, 0, 0), PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2", OrderTotal=75,NumTotalTickets=4},
        };

        private List<OrderTicket> MockTickets = new List<OrderTicket>() {

            new OrderTicket{ OrderId=1, TicketTypeId=2,TypeName="VIP", Quantity=2, Price=50 },
            new OrderTicket{ OrderId=2, TicketTypeId=2,TypeName="VIP", Quantity=3, Price=25 },
            new OrderTicket{ OrderId=2, TicketTypeId=1,TypeName="Free", Quantity=1, Price=0 },
            };


        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            await Task.Delay(10);
            //  var token = await GetUserTokenAsync();
            string token = "Temp"; // I dont wan't to test tokerService

            if (!string.IsNullOrEmpty(token))
            {

                return MockOrders.FirstOrDefault(o => o.OrderId == orderId);
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

     
        public async Task<List<Order>> GetOrdersByUserNameAsync(string userName, int page, int take)
        {
            await Task.Delay(10);
            //  var token = await GetUserTokenAsync();
            string token = "Temp"; // I dont wan't to test tokerService

            if (!string.IsNullOrEmpty(token))
            {

                return MockOrders.Where(o => o.UserName == userName).ToList();
            }
            else
                return new List<Order>();
        }
        #endregion
    }
}