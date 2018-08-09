using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventTicketAPI.Data;
using EventTicketAPI.Domain;
using EventTicketAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EventTicketAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Ticket")]
    public class TicketController : Controller
    {
        private readonly TicketCatalogContext _ticketCatalogContext;
        private readonly IOptionsSnapshot<TicketSetting> _settings;
        public TicketController(TicketCatalogContext ticketCatalogContext, IOptionsSnapshot<TicketSetting> settings)
        {
            _ticketCatalogContext = ticketCatalogContext;
            _settings = settings;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> TicketTypes()
        {
            var items = await _ticketCatalogContext.TicketTypes.ToListAsync();
            return Ok(items);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Tickets([FromQuery] int pageSize = 6,
                                                [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _ticketCatalogContext.Tickets
                                    .LongCountAsync();
            var itemsOnPage = await _ticketCatalogContext.Tickets
                                        .OrderBy(c => c.EventTitle)
                                        .Skip(pageSize * pageIndex)
                                        .Take(pageSize)
                                        .ToListAsync();
            var model = new PaginatedEventTicketViewModel<Ticket>
                   (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }
        [HttpGet]
        [Route("Tickets/{id:int}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var item = await _ticketCatalogContext.Tickets
                .SingleOrDefaultAsync(c => c.Id == id);
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound();
        }
        //GET api/Tickets/withTitle/Burger Fest?pageSize=2&pageIndex=0
        [HttpGet]
        [Route("[action]/withtitle/{title:minlength(1)}")]
        public async Task<IActionResult> GetTicketsByEventTitle(string title,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _ticketCatalogContext.Tickets
                                    .Where(c => c.EventTitle.StartsWith(title))
                                    .LongCountAsync();
            var itemsOnPage = await _ticketCatalogContext.Tickets
                                    .Where(c => c.EventTitle.StartsWith(title))
                                    .OrderBy(c => c.EventTitle)
                                    .Skip(pageSize * pageIndex)
                                    .Take(pageSize)
                                    .ToListAsync();

            var model = new PaginatedEventTicketViewModel<Ticket>
                    (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }
        [HttpGet]
        [Route("tickets/eventid/{eventId:int}")]

        public async Task<IActionResult> GetTicketsbyEventId(int eventId,
               [FromQuery] int pageSize = 6,
               [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _ticketCatalogContext.Tickets
                               .Where(c => c.EventId == eventId)
                              .LongCountAsync();

            var itemsOnPage = await _ticketCatalogContext.Tickets
                              .Where(c => c.EventId == eventId)
                              .OrderBy(c => c.EventTitle)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();


            var model = new PaginatedEventTicketViewModel<Ticket>

               (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);

        }
        [HttpGet]
        [Route("tickets/tickettypeid/{typeId:int}")]

        public async Task<IActionResult> GetTicketsByTicketTypeId(int typeId,
               [FromQuery] int pageSize = 6,
               [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _ticketCatalogContext.Tickets
                               .Where(c => c.TicketTypeId == typeId)
                              .LongCountAsync();

            var itemsOnPage = await _ticketCatalogContext.Tickets
                              .Where(c => c.EventId == typeId)
                              .OrderBy(c => c.EventTitle)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();


            var model = new PaginatedEventTicketViewModel<Ticket>

               (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);

        }

        [HttpGet]
        [Route("tickets/ByAvailableQuantity/{availableQuantity:int}")]

        public async Task<IActionResult> GetTicketsByAvailableQuantity(int availableQuantity,
              [FromQuery] int pageSize = 6,
              [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _ticketCatalogContext.Tickets
                               .Where(c => c.AvailableQty >= availableQuantity)
                              .LongCountAsync();

            var itemsOnPage = await _ticketCatalogContext.Tickets
                              .Where(c => c.AvailableQty >= availableQuantity)
                              .OrderBy(c => c.EventTitle)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();


            var model = new PaginatedEventTicketViewModel<Ticket>

               (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);

        }

        //commands

        [HttpPost]

        [Route("tickets")]
        public async Task<IActionResult> CreateTicket([FromBody] Ticket newTicket)
        {
            var item = new Ticket

            {
                EventId = newTicket.EventId,
                EventTitle = newTicket.EventTitle,
                TicketPrice = newTicket.TicketPrice,
                AvailableQty = newTicket.AvailableQty,
                TicketTypeId = newTicket.TicketTypeId,
                MinTktsPerOrder = newTicket.MinTktsPerOrder,
                MaxTktsPerOrder = newTicket.MaxTktsPerOrder,
                SalesStartDate = newTicket.SalesStartDate,
                SalesEndDate = newTicket.SalesEndDate


            };
            _ticketCatalogContext.Tickets.Add(item);
            await _ticketCatalogContext.SaveChangesAsync();
            var result = GetTicketById(item.Id);
            return Ok(result);
           // return CreatedAtAction(nameof(GetTicketById), new { id = item.Id });
        }
        [HttpPut]

        [Route("tickets")]
        public async Task<IActionResult> UpdateTicket([FromBody] Ticket ticketToUpdate)
        {
            var TicketItem = await _ticketCatalogContext.Tickets
                              .SingleOrDefaultAsync
                              (i => i.Id == ticketToUpdate.Id);
            if (TicketItem == null)
            {
                return NotFound(new { Message = $"Item with id {ticketToUpdate.Id} not found." });
            }
            
            TicketItem.EventId = ticketToUpdate.EventId;
            TicketItem.EventTitle = ticketToUpdate.EventTitle;
            TicketItem.TicketPrice = ticketToUpdate.TicketPrice;
            TicketItem.AvailableQty = ticketToUpdate.AvailableQty;
            TicketItem.TicketTypeId = ticketToUpdate.TicketTypeId;
            TicketItem.MinTktsPerOrder = ticketToUpdate.MinTktsPerOrder;
            TicketItem.MaxTktsPerOrder = ticketToUpdate.MaxTktsPerOrder;
            TicketItem.SalesStartDate = ticketToUpdate.SalesStartDate;
            TicketItem.SalesEndDate = ticketToUpdate.SalesEndDate;
            _ticketCatalogContext.Tickets.Update(TicketItem);
            await _ticketCatalogContext.SaveChangesAsync();
            var result = GetTicketById(ticketToUpdate.Id);
            return Ok(result);
            //return CreatedAtAction(nameof(GetTicketById), new { id = ticketToUpdate.Id });
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticketItem = await _ticketCatalogContext.Tickets
                .SingleOrDefaultAsync(p => p.Id == id);
            if (ticketItem == null)
            {
                return NotFound();

            }
            _ticketCatalogContext.Tickets.Remove(ticketItem);
            await _ticketCatalogContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete]
        [Route("deletealltickets/{eventid}")]
        public async Task<IActionResult> DeleteTicketsByEventId(int eventid)
        {
            var ticketItem = await _ticketCatalogContext.Tickets
                .Where(p => p.EventId == eventid).ToListAsync();
            if (ticketItem == null)
            {
                return NotFound(new { Message = $"Tickets with eventid {eventid} not found." });

            }
            _ticketCatalogContext.Tickets.RemoveRange(ticketItem);
            await _ticketCatalogContext.SaveChangesAsync();
            return NoContent();

        }

    }
}




