using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;
using WebMvc.Models.CartModels;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;
using WebMvc;
using WebMvc.Infrastructure;

using WebMvc.Models.Orders;

namespace WebMvc.Services
{
    public class CartService : ICartService
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private IHttpClient _apiClient;
        private readonly string _remoteServiceBaseUrl;
        private IHttpContextAccessor _httpContextAccesor;
        private readonly ILogger _logger;
        public CartService(IOptionsSnapshot<AppSettings> settings, IHttpContextAccessor httpContextAccesor, IHttpClient httpClient, ILoggerFactory logger)
        {
            _settings = settings;
            _remoteServiceBaseUrl = $"{_settings.Value.CartUrl}/api/v1/cart";
            _httpContextAccesor = httpContextAccesor;
            _apiClient = httpClient;
            _logger = logger.CreateLogger<CartService>();
        }


        public async Task AddItemToCart(ApplicationUser user, CartItem product)
        {
            var cart = await GetCart(user);
            _logger.LogDebug("User Name: " + user.Id);
            if (cart == null)
            {
                cart = new Cart()
                {
                    BuyerId = user.Id,
                    Items = new List<CartItem>()
                };
            }
            var cartbasketItem = cart.Items
                .Where(p => p.OrderId == product.OrderId)
                .FirstOrDefault();
            if (cartbasketItem == null)
            {
                cart.Items.Add(product);
            }
            else
            {
                cartbasketItem.Quantity += 1;
            }


            await UpdateCart(cart);
        }

        public async Task ClearCart(ApplicationUser user)
        {
            var token = await GetUserTokenAsync();
            var cleancartBasketUri = ApiPaths.CartBasket.CleanBasket(_remoteServiceBaseUrl, user.Id);
            _logger.LogDebug("Clean Basket uri : " + cleancartBasketUri);
            var response = await _apiClient.DeleteAsync(cleancartBasketUri);
            _logger.LogDebug("CartBasket cleaned");
        }

        public async Task<Cart> GetCart(ApplicationUser user)
        {
            var token = await GetUserTokenAsync();
            _logger.LogInformation(" We are in get cartbasket and user id " + user.Id);
            _logger.LogInformation(_remoteServiceBaseUrl);

            var getcartBasketUri = ApiPaths.CartBasket.GetBasket(_remoteServiceBaseUrl, user.Id);
            _logger.LogInformation(getcartBasketUri);
            var dataString = await _apiClient.GetStringAsync(getcartBasketUri, token);
            _logger.LogInformation(dataString);

            var response = JsonConvert.DeserializeObject<Cart>(dataString.ToString()) ??
               new Cart()
               {
                   BuyerId = user.Id
               };
            return response;
        }

        public Order MapCartToOrder(Cart cart)
        {
            var order = new Order();

            order.BuyerId = cart.BuyerId;
            order.OrderId = DateTime.Now.Millisecond;
            cart.Items.ForEach(x =>
            {
                order.OrderTicket.Add(new OrderTicket()
                {
                    OrderId = order.OrderId,
                    EventId = x.EventId,
                    EventTitle = x.Title,
                    ImageUrl = x.ImageUrl,
                    TicketTypeId = x.TicketTypeId,
                    TypeName = x.TicketType,
                    
                    Quantity = x.Quantity,
                    Price = x.Price
                });
                order.OrderTotal += (x.Quantity * x.Price);
            });

            return order;
        }


        public async Task<Cart> SetQuantities(ApplicationUser user, Dictionary<int, int> quantities)
        {
            var cartbasket = await GetCart(user);

            cartbasket.Items.ForEach(x =>
            {
                // Simplify this logic by using the
                // new out variable initializer.
                if (quantities.TryGetValue(x.OrderId, out var quantity))
                {
                    x.Quantity = quantity;
                }
            });

            return cartbasket;
        }

        public async Task<Cart> UpdateCart(Cart cart)
        {

              var token = await GetUserTokenAsync();
            _logger.LogDebug("Service url: " + _remoteServiceBaseUrl);
            var updatecartBasketUri = ApiPaths.CartBasket.UpdateBasket(_remoteServiceBaseUrl);
            _logger.LogDebug("Update Basket url: " + updatecartBasketUri);
            var response = await _apiClient.PostAsync(updatecartBasketUri, cart,token); 
            response.EnsureSuccessStatusCode();

            return cart;
        }

        async Task<string> GetUserTokenAsync()
        {
            var context = _httpContextAccesor.HttpContext;

            return await context.GetTokenAsync("access_token");
          
        }
    }
}
