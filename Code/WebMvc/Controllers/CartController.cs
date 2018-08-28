using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;
using WebMvc.Services;
using WebMvc.Models;
using WebMvc.Models.CartModels;
using Polly.CircuitBreaker;


namespace WebMvc.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        
        private readonly ICartService _cartService;
        private readonly IEventCatalogService _catalogService;
        private readonly IIdentityService<ApplicationUser> _identityService;

        public CartController(IIdentityService<ApplicationUser> identityService, ICartService cartService, IEventCatalogService catalogService)
        {
            _identityService = identityService;
            _cartService = cartService;
            _catalogService = catalogService;



        }
        public    IActionResult  Index()
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
            //    //}

                return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<int, int> quantities, string action)
        {
            if (action == " CHECKOUT ")
            {
                return RedirectToAction("Create", "Orders");
            }


            try
            {
                var user = _identityService.Get(HttpContext.User);
                var cartbasket = await _cartService.SetQuantities(user, quantities);
                var vm = await _cartService.UpdateCart(cartbasket);

            }
            catch (BrokenCircuitException)
            {
                // Catch error when CartApi is in open circuit  mode                 
                HandleBrokenCircuitException();
            }

            return View();

        }

            public async Task<IActionResult> AddToCart(Event productDetails)
        {
            try
            {
                if (productDetails.Id.ToString() != null)
                {
                    var user = _identityService.Get(HttpContext.User);
                    var product = new CartItem()
                    {
                        OrderId = (int)DateTime.Now.Ticks,
                        Quantity = 1,
                        ImageUrl = productDetails.ImageUrl,
                        Price = productDetails.Price,
                        EventId = productDetails.Id,
                        Title = productDetails.Title,
                        Address = productDetails.Address,
                        City = productDetails.City,
                        State = productDetails.State,
                        Zipcode = productDetails.Zipcode,
                        StartDate = productDetails.StartDate,
                        EventCategory = productDetails.EventCategory,
                        EventCategoryId = productDetails.EventCategoryId,
                        EventType = productDetails.EventType,
                        EventDescription = productDetails.EventDescription,
                        OrganizerId = productDetails.OrganizerId,
                        OrganizerName = productDetails.OrganizerName

                    };
                    await _cartService.AddItemToCart(user, product);
                    //await _cartService.ClearCart(user);
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
        //public async Task WriteOutIdentityInfo()
        //{
        //    var identityToken =
        //        await HttpContext.Authentication.
        //         GetAuthenticateInfoAsync(OpenIdConnectParameterNames.IdToken);
        //    Debug.WriteLine($"Identity Token: {identityToken}");
        //    foreach (var claim in User.Claims)
        //    {
        //        Debug.WriteLine($"Claim Type: {claim.Type} - Claim Value : {claim.Value}");
        //    }

        //}

        private void HandleBrokenCircuitException()
        {
            TempData["CartBasketInoperativeMsg"] = "cart Service is inoperative, please try later on. (Business Msg Due to Circuit-Breaker)";
        }

    }
}