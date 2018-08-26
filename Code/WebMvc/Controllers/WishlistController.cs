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

       
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddToWishlist(WishlistItem eventDetails)
        {
            try
            {
                if (eventDetails.Id != null)
                {
                    var user = _identityService.Get(HttpContext.User);
                    var product = new WishlistItem()
                    {
                        Id = Guid.NewGuid().ToString(),
                        EventTitle = eventDetails.EventTitle,
                        ImageUrl = eventDetails.ImageUrl,
                        TicketPrice = eventDetails.TicketPrice,
                        City = eventDetails.City,
                        TicketType=eventDetails.TicketType
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