using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;
using WebMvc.Models.WishlistModels;
using WebMvc.Services;

namespace WebMvc.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly IEventCatalogService _ecatalogService;
        private readonly IIdentityService<ApplicationUser> _identityService;

        public WishlistController(IIdentityService<ApplicationUser> identityService, IWishlistService wishlistService, IEventCatalogService ecatalogService)
        {
            _identityService = identityService;
            _wishlistService = wishlistService;
            _ecatalogService = ecatalogService;



        }
        public IActionResult Index()
        {
            //try
            //{

            //    var user = _identityService.Get(HttpContext.User);
            //    var cart = await _cartService.GetCart(user);


            //    return View();
            //}
            //catch (BrokenCircuitException)
            //{
            //    // Catch error when CartApi is in circuit-opened mode                 
            //    HandleBrokenCircuitException();
            //}

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index( string action)
        {
           /* if (action == "[ Checkout ]")
            {
                return RedirectToAction("Create", "Order");
            }*/


            try
            {
                var user = _identityService.Get(HttpContext.User);
                var basket = await _wishlistService.GetWishlist(user);
                var vm = await _wishlistService.UpdateWishlist(basket);

            }
            catch (BrokenCircuitException)
            {
                // Catch error when CartApi is in open circuit  mode                 
                HandleBrokenCircuitException();
            }

            return View();
        }

        private void handlebrokencircuitexception()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> AddToWishlist(Event eventDetails)
        {
            try
            {
                if (eventDetails.Id != null)
                {
                    var user = _identityService.Get(HttpContext.User);
                    var product = new WishlistItem()
                    {
                        Id = Guid.NewGuid().ToString(),
                        EventTitle = eventDetails.Title,
                        ImageUrl = eventDetails.ImageUrl,
                        TicketPrice = eventDetails.Price,
                        City = eventDetails.City,
                        productId=eventDetails.Id,
                    };
                    await _wishlistService.AddItemToWishlist(user, product);
                }
                return RedirectToAction("Index", "EventCatalog");
            }
            catch (BrokenCircuitException)
            {
                // Catch error when CartApi is in circuit-opened mode               
  
                HandleBrokenCircuitException();
            }

            return RedirectToAction("Index", "EventCatalog");

        }

        private void HandleBrokenCircuitException()
        {
            TempData["BasketInoperativeMsg"] = "cart Service is inoperative, please try later on. (Business Msg Due to Circuit-Breaker)";
        }

    }
}