using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventCatalogAPI.Controllers
{
    //chitra
    [Produces("application/json")]
    [Route("api/Event")]
    public class EventController : Controller
    {
        private readonly EventCatalogContext _eventCatalogContext;

        public EventController (EventCatalogContext eventCatalogContext)
        {
            _eventCatalogContext = eventCatalogContext;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> EventTypes()
        {
            var items = await _eventCatalogContext.EventTypes.ToListAsync();
            return Ok(items);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventCategories()
        {
            var items = await _eventCatalogContext.EventCategories.ToListAsync();
            return Ok(items);
        }

    }
}