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
        public async Task<IActionResult> GetEventById(int id)
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
        //GET api/Events/withTitle/Burger Fest?pageSize=2&pageIndex=0
        [HttpGet]
        [Route("[action]/withtitle/{title:minlength(1)}")]
        public async Task<IActionResult> Tickets(string title,
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





    }
}