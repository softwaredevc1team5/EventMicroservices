using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Controllers;
using WebMvc.Models;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.ViewComponents
{
    public class WishlistList : ViewComponent
    {
        private readonly IWishlistService _wishlistsvc;
        public WishlistList(IWishlistService wishlistsvc) => _wishlistsvc = wishlistsvc;
        public async Task<IViewComponentResult> InvokeAsync(ApplicationUser user)
        {
            var vm = new Models.WishlistModels.Wishlist();
            
                try
                {
                    vm = await _wishlistsvc.GetWishlist(user);
                    return View(vm);
                }
                catch (BrokenCircuitException)
                {
                    // Catch error when CartApi is in open circuit mode
                    ViewBag.IsBasketInoperative = true;
                    TempData["BasketInoperativeMsg"] = "Basket Service is inoperative, please try later on. (Business Msg Due to Circuit-Breaker)";

                }

                return View(vm);

            
        }
    }
}
