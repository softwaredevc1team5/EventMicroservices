using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Controllers
{

    [Produces("application/json")]
    [Route("api/Event")]
    public class EventController : Controller
    {
        private readonly EventCatalogContext _eventCatalogContext;
        private readonly IOptionsSnapshot<EventSettings> _settings;
        public EventController(EventCatalogContext eventCatalogContext, IOptionsSnapshot<EventSettings> settings)
        {
            _eventCatalogContext = eventCatalogContext;
            _settings = settings;
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

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Events([FromQuery] int pageSize =6,
                                                [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _eventCatalogContext.Events
                                    .LongCountAsync();
            var itemsOnPage = await _eventCatalogContext.Events
                                        .OrderBy(c => c.Title)
                                        .Skip(pageSize * pageIndex)
                                        .Take(pageSize)
                                        .ToListAsync();
            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
            var model = new PaginatedEventViewModel<Event>
                   (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        private List<Event> ChangeUrlPlaceHolder(List<Event> items)
        {
            items.ForEach(
                x => x.ImageUrl =
                x.ImageUrl
                .Replace("http://externalcatalogbaseurltobereplaced",
                _settings.Value.ExternalCatalogBaseUrl));

            return items;
        }

        [HttpGet]
        [Route("Events/{id:int}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var item = await _eventCatalogContext.Events
                .SingleOrDefaultAsync(c => c.Id == id);
            if (item != null)
            {
                item.ImageUrl = item.ImageUrl
                 .Replace("http://externalcatalogbaseurltobereplaced",
                _settings.Value.ExternalCatalogBaseUrl);
                return Ok(item);
            }
            return NotFound();
        }

        //GET api/Events/withTitle/Burger Fest?pageSize=2&pageIndex=0
        [HttpGet]
        [Route("[action]/withtitle/{title:minlength(1)}")]
        public async Task<IActionResult> Events(string title,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _eventCatalogContext.Events
                                    .Where(c => c.Title.StartsWith(title))
                                    .LongCountAsync();
            var itemsOnPage = await _eventCatalogContext.Events
                                    .Where(c => c.Title.StartsWith(title))
                                    .OrderBy(c => c.Title)
                                    .Skip(pageSize * pageIndex)
                                    .Take(pageSize) 
                                    .ToListAsync();
            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
            var model = new PaginatedEventViewModel<Event>
                    (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }
        [HttpGet]
        [Route("Events/withcity/{city:minlength(1)}")]
        public async Task<IActionResult> EventsWithCity(string city,
          [FromQuery] int pageSize = 6,
          [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _eventCatalogContext.Events
                                    .Where(c => c.City.StartsWith(city))
                                    .LongCountAsync();
            var itemsOnPage = await _eventCatalogContext.Events
                                    .Where(c => c.City.StartsWith(city))
                                    .OrderBy(c => c.Title)
                                    .Skip(pageSize * pageIndex)
                                    .Take(pageSize)
                                    .ToListAsync();
            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
            var model = new PaginatedEventViewModel<Event>
                    (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        //GET api/Events/type/1/category/null[?pageSize=4&pageIndex=0]
        [HttpGet]
        [Route("[action]/type/{eventTypeId}/brand/{eventCategoryId}")]

        public async Task<IActionResult> Events(int? eventTypeId, int? eventCategoryId, [FromQuery] int pageSize = 6,
                                                                              [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<Event>)_eventCatalogContext.Events;
            if (eventTypeId.HasValue)
            {
                root = root.Where(c => c.EventTypeId == eventTypeId);
            }
            if (eventCategoryId.HasValue)
            {
                root = root.Where(c => c.EventCategoryId == eventCategoryId);
            }
            var totalItems = await root
                                .LongCountAsync();
            var itemsOnPage = await root
                                .OrderBy(c => c.Title)
                                .Skip(pageSize * pageIndex)
                                .Take(pageSize)
                                .ToListAsync();
            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
            var model = new PaginatedEventViewModel<Event>(pageIndex, pageSize, totalItems, itemsOnPage);
            return Ok(model);
        }

        [HttpPost]
        [Route("events")]
        public async Task<IActionResult> CreateEvent([FromBody] Event newEvent)
        {

            var item = new Event
            {
                
                Title =  newEvent.Title,
                OrganizerId = newEvent.OrganizerId,
                Address = newEvent.Address,
                City = newEvent.City,
                State = newEvent.State,
                Zipcode = newEvent.Zipcode,
                ImageUrl = newEvent.ImageUrl,
                Price=newEvent.Price,
                StartDate = newEvent.StartDate,
                EndDate = newEvent.EndDate,
                EventCategoryId = newEvent.EventCategoryId,
                EventTypeId = newEvent.EventTypeId,
                OrganizerName=newEvent.OrganizerName
                
                
                
            };
            _eventCatalogContext.Events.Add(item);
            await _eventCatalogContext.SaveChangesAsync();
            var result = GetEventById(item.Id);
            return Ok(result);
           // return CreatedAtAction(nameof(GetEventById), new { id = item.Id });
        }


        [HttpPut]
        [Route("events")]
        public async Task<IActionResult> UpdateEvent(
            [FromBody] Event eventToUpdate)
        {
            var catalogEvent = await _eventCatalogContext.Events
                              .SingleOrDefaultAsync
                              (i => i.Id == eventToUpdate.Id);
            if (catalogEvent == null)
            {
                return NotFound(new { Message = $"Item with id {eventToUpdate.Id} not found." });
            }
            catalogEvent = eventToUpdate;
            _eventCatalogContext.Events.Update(catalogEvent);
            await _eventCatalogContext.SaveChangesAsync();
            var result = GetEventById(eventToUpdate.Id);
            return Ok(result);

            // return CreatedAtAction(nameof(GetEventById), new { id = eventToUpdate.Id });
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var catalogEvent = await _eventCatalogContext.Events
                .SingleOrDefaultAsync(p => p.Id == id);
            if (catalogEvent == null)
            {
                return NotFound();

            }
            _eventCatalogContext.Events.Remove(catalogEvent);
            await _eventCatalogContext.SaveChangesAsync();
            return NoContent();

        }

    }
}