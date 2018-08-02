using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventTicketAPI.Data;
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

    }
}