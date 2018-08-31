using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;
using WebMvc.Models.WishlistModels;

namespace WebMvc.Services
{
    public interface IWishlistService
    {
        Task<Wishlist> GetWishlist(ApplicationUser user);
        Task AddItemToWishlist(ApplicationUser user, WishlistItem product);
        Task<Wishlist> UpdateWishlist(Wishlist Wishlist);
       // Task<Wishlist> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities);
        //Order MapCartToOrder(Wishlist Wishlist);
        Task ClearWishlist(ApplicationUser user);
    }
}
