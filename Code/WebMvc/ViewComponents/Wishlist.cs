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
    public class Wishlist: ViewComponent
    {
        private readonly IWishlistService _wishlistsvc;
        public Wishlist(IWishlistService wishlistsvc) => _wishlistsvc = wishlistsvc;
        public async Task<IViewComponentResult> InvokeAsync(ApplicationUser user)
        {
            var vm = new WishlistComponentViewModel();
            try
            {
                var wishlist = await _wishlistsvc.GetWishlist(user);

                vm.ItemsInWishlist = wishlist.Items.Count;
                return View(vm);
            }
            catch (BrokenCircuitException)
            {
                // Catch error when CartApi is in open circuit mode
                ViewBag.IsBasketInoperative = true;
            }

            return View(vm);
        }

    }

}
