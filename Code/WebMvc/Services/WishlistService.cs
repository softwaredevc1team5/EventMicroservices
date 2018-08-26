using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Infrastructure;
using WebMvc.Models;
using WebMvc.Models.WishlistModels;

namespace WebMvc.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private IHttpClient _apiClient;
        private readonly string _remoteServiceBaseUrl;
        private IHttpContextAccessor _httpContextAccesor;
        private readonly ILogger _logger;
        public WishlistService(IOptionsSnapshot<AppSettings> settings, IHttpContextAccessor httpContextAccesor, IHttpClient httpClient, ILoggerFactory logger)
        {
            _settings = settings;
            _remoteServiceBaseUrl = $"{_settings.Value.WishlistUrl}/api/v1/whislist";
            _httpContextAccesor = httpContextAccesor;
            _apiClient = httpClient;
            _logger = logger.CreateLogger<WishlistService>();
        }
        public  async Task AddItemToWishlist(ApplicationUser user, WishlistItem product)
        {
            var wishlist = await GetWishlist(user);
            _logger.LogDebug("User Name: " + user.Id);
            if (wishlist == null)
            {
                wishlist = new Wishlist()
                {
                    BuyerId = user.Id,
                    Items = new List<WishlistItem>()
                };
            }
            var basketItem = wishlist.Items
                .Where(p => p.productId == product.productId)
                .FirstOrDefault();
            if (basketItem == null)
            {
                wishlist.Items.Add(product);
            }


            await UpdateWishlist(wishlist);


        }

        public async  Task ClearWishlist(ApplicationUser user)
        {
            var token = await GetUserTokenAsync();
            var cleanBasketUri = ApiPaths.Basket.CleanBasket(_remoteServiceBaseUrl, user.Id);
            _logger.LogDebug("Clean Basket uri : " + cleanBasketUri);
            var response = await _apiClient.DeleteAsync(cleanBasketUri);
            _logger.LogDebug("Basket cleaned");
        }

        public async Task<Wishlist> GetWishlist(ApplicationUser user)
        {
            var token = await GetUserTokenAsync();
            _logger.LogInformation(" We are in get basket and user id " + user.Id);
            _logger.LogInformation(_remoteServiceBaseUrl);

            var getBasketUri = ApiPaths.Basket.GetBasket(_remoteServiceBaseUrl, user.Id);
            _logger.LogInformation(getBasketUri);
            var dataString = await _apiClient.GetStringAsync(getBasketUri, token);
            //var dataString = "";
            _logger.LogInformation(dataString);

            var response = JsonConvert.DeserializeObject<Wishlist>(dataString.ToString()) ??
               new Wishlist()
               {
                   BuyerId = user.Id
               };
            return response;
        }
        public async  Task<Wishlist> UpdateWishlist(Wishlist wishlist)
        {
            var token = await GetUserTokenAsync();
            _logger.LogDebug("Service url: " + _remoteServiceBaseUrl);
            var updateBasketUri = ApiPaths.Basket.UpdateBasket(_remoteServiceBaseUrl);
            _logger.LogDebug("Update Basket url: " + updateBasketUri);
            var response = await _apiClient.PostAsync(updateBasketUri, wishlist, token);
            response.EnsureSuccessStatusCode();

            return wishlist;
        }

        async  Task<string> GetUserTokenAsync()
        {
            var context = _httpContextAccesor.HttpContext;

            return await context.GetTokenAsync("access_token");
        }
    }
}
