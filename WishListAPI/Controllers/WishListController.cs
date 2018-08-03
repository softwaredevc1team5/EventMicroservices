using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WishListAPI.Data;
using WishListAPI.Domain;
using WishListAPI.ViewModels;

namespace WishListAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/WishList")]
    public class WishListController : Controller
    {
        private readonly WishListContext _wishListContext;

        public WishListController(WishListContext wishListContext)
        {
            _wishListContext = wishListContext;
        }

        //get all wishList events
        //GET api/WishList/events/
        [HttpGet]
        [Route("[action]")]

        public async Task<IActionResult> Events(
           [FromQuery] int pageSize = 6,
           [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _wishListContext.WishCartItems.LongCountAsync();

            var itemsOnPage = await _wishListContext.WishCartItems
                              .OrderBy(c => c.EventTitle)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();

            // itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);

            var model = new PaginatedWishListViewModel<WishCartItem>
                (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);

        }



        [HttpGet]
        [Route("events/{id:int}")]
        public async Task<IActionResult> GetWishListCartItemById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var item = await _wishListContext.WishCartItems
                .SingleOrDefaultAsync(c => c.Id == id);

            if (item != null)
            {
                return Ok(item);
            }
            return NotFound();

        }

        //get wishlist by event title
        //GET api/WishList/events/withtitle/Feed the Children Gala?pageSize=2&pageIndex=0

        [HttpGet]
        [Route("[action]/withtitle/{title:minlength(1)}")]

        public async Task<IActionResult> Events(string title,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _wishListContext.WishCartItems
                               .Where(c => c.EventTitle.StartsWith(title))
                              .LongCountAsync();

            var itemsOnPage = await _wishListContext.WishCartItems

                              .Where(c => c.EventTitle.StartsWith(title))
                              .OrderBy(c => c.EventTitle)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();

            //  itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);

            var model = new PaginatedWishListViewModel<WishCartItem>

               (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);

        }

        //get wishlist by ticketype
        //GET api/WishList/events/withtickettype/Free?pageSize=2&pageIndex=0

        [HttpGet]
        [Route("events/withtickettype/{ticketType:minlength(1)}")]

        public async Task<IActionResult> EventsByTicketType(string ticketType,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _wishListContext.WishCartItems
                               .Where(c => c.TicketType.StartsWith(ticketType))
                              .LongCountAsync();

            var itemsOnPage = await _wishListContext.WishCartItems

                              .Where(c => c.TicketType.StartsWith(ticketType))
                              .OrderBy(c => c.EventTitle)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();


            var model = new PaginatedWishListViewModel<WishCartItem>

               (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);

        }


        //get wishlist by eventid
        //GET api/WishList/events/witheventid/1?pageSize=2&pageIndex=0

        [HttpGet]
        [Route("[action]/witheventid/{eventid:int}")]

        public async Task<IActionResult> Events(int eventid,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _wishListContext.WishCartItems
                               .Where(c => c.EventId == eventid)
                              .LongCountAsync();

            var itemsOnPage = await _wishListContext.WishCartItems
                              .Where(c => c.EventId == eventid)
                              .OrderBy(c => c.EventTitle)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();


            var model = new PaginatedWishListViewModel<WishCartItem>

               (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);

        }

        //get wishlist by buyerid
        //GET api/WishList/events/withbuyerid/1?pageSize=2&pageIndex=0

        [HttpGet]
        [Route("events/withbuyerid/{buyerid:int}")]

        public async Task<IActionResult> EventsWithBuyerId(int buyerid,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _wishListContext.WishCartItems
                               .Where(c => c.BuyerId == buyerid)
                              .LongCountAsync();

            var itemsOnPage = await _wishListContext.WishCartItems
                              .Where(c => c.BuyerId == buyerid)
                              .OrderBy(c => c.EventTitle)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();


            var model = new PaginatedWishListViewModel<WishCartItem>

               (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);

        }

        //commands

        [HttpPost]

        [Route("events")]

        public async Task<IActionResult> AddWishCartItem(
            [FromBody] WishCartItem wishCartItem)
        {
            var item = new WishCartItem
            {
                BuyerId = wishCartItem.BuyerId,
                EventId = wishCartItem.EventId,
                EventTitle = wishCartItem.EventTitle,
                TicketPrice = wishCartItem.TicketPrice,
                NumOfTickets = wishCartItem.NumOfTickets,
                TicketType = wishCartItem.TicketType
            };

            _wishListContext.WishCartItems.Add(item);

            await _wishListContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWishListCartItemById), new { id = item.Id });

        }

        [HttpPut]

        [Route("events")]

        public async Task<IActionResult> UpdateWishCartItem(
            [FromBody] WishCartItem wishListEventToUpdate)
        {
            var wishListItem = await _wishListContext.WishCartItems
                              .SingleOrDefaultAsync
                              (i => i.Id == wishListEventToUpdate.Id);
            if (wishListItem == null)
            {
                return NotFound(new { Message = $"Event with id {wishListEventToUpdate.Id} not found." });
            }

            wishListItem = wishListEventToUpdate;

            _wishListContext.WishCartItems.Update(wishListItem);

            await _wishListContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWishListCartItemById), new { id = wishListEventToUpdate.Id });
        }


        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteWishCartItem(int id)
        {
            var wishCartItem = await _wishListContext.WishCartItems

                .SingleOrDefaultAsync(w => w.Id == id);

            if (wishCartItem == null)
            {
                return NotFound();
            }

            _wishListContext.WishCartItems.Remove(wishCartItem);

            await _wishListContext.SaveChangesAsync();

            return NoContent();

        }


        [HttpDelete]
        [Route("emptywishlist/{buyerid}")]
        public async Task<IActionResult> EmptyWishCartItem(int buyerid)
        {

            var wishcartitems = await _wishListContext.WishCartItems.Where(c => c.BuyerId == buyerid).ToListAsync();

            _wishListContext.WishCartItems.RemoveRange(wishcartitems);

            await _wishListContext.SaveChangesAsync();

            return NoContent();

        }

    }
}
