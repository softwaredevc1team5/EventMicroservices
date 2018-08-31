using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WishListAPI.Model;

namespace WishListAPI.Controllers
{
    [Route("api/v1/[controller]")]
    public class WishlistController : Controller
    {
        private IWishlistRepository _repository;
        private ILogger _logger;
        public WishlistController(IWishlistRepository repository, ILoggerFactory factory)
        {
            _repository = repository;
            _logger = factory.CreateLogger<WishlistController>();
        }
        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Wishlist), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string id)
        {
            var basket = await _repository.GetWishlistAsync(id);

            return Ok(basket);
        }

        // POST api/values
        [HttpPost]

        [ProducesResponseType(typeof(Wishlist), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]Wishlist value)
        {
            _logger.LogInformation("Received a request to save wishList: ", value.BuyerId);
            var basket = await _repository.UpdateWishlistAsync(value);

            return Ok(basket);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _logger.LogInformation("Delete method in Cart controller reached");
            _repository.DeleteWishlistAsync(id);


        }

    }
}