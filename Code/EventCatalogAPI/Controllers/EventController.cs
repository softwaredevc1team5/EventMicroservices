using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
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
        public async Task<IActionResult> EventCategoriesForImage([FromQuery] int pageSize = 6,
                                                [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _eventCatalogContext.EventCategories
                                    .LongCountAsync();
            var itemsOnPage = await _eventCatalogContext.EventCategories
                                        .OrderBy(c => c.Name)
                                        .Skip(pageSize * pageIndex)
                                        .Take(pageSize)
                                        .ToListAsync();
            
            itemsOnPage = ChangeUrlPlaceHolderForCategory(itemsOnPage);
            var model = new PaginatedEventViewModel<EventCategory>
                   (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
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

        private List<EventCategory> ChangeUrlPlaceHolderForCategory(List<EventCategory> items)
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
        [Route("Events/date/{date}")]
        public async Task<IActionResult> EventsWithDate(DateTime date,
         [FromQuery] int pageSize = 6,
         [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _eventCatalogContext.Events
                                    .Where(c => c.StartDate == date)
                                    .LongCountAsync();
            var itemsOnPage = await _eventCatalogContext.Events
                                    .Where(c => c.StartDate == date)
                                    .OrderBy(c => c.Title)
                                    .Skip(pageSize * pageIndex)
                                    .Take(pageSize)
                                    .ToListAsync();
            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
            var model = new PaginatedEventViewModel<Event>
                    (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }
        

        //GET api/Events/eventtype/1/eventcategory/null?pageSize=4&pageIndex=0
        [HttpGet]
        [Route("[action]/eventtype/{eventTypeId}/eventcategory/{eventCategoryId}")]

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

        //EventCity get methods - Bhuvana code start
        private List<EventCity> ChangeUrlPlaceHolder(List<EventCity> items)
        {
            items.ForEach(
                x => x.CityImageUrl =
                x.CityImageUrl
                .Replace("http://externalcatalogbaseurltobereplaced",
                _settings.Value.ExternalCatalogBaseUrl));

            return items;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventCities([FromQuery] int pageSize = 6,
                                                [FromQuery] int pageIndex = 0)
        {
            var items = await _eventCatalogContext.EventCities.ToListAsync();
            return Ok(items);
        }
        [HttpGet]
        [Route("Events/withcity/{city:minlength(1)}")]
        public async Task<IActionResult> EventsWithCity(string city,
          [FromQuery] int pageSize = 6,
          [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<Event>)_eventCatalogContext.Events;
            var totalItems = await root
                                     .Where(c => c.City.StartsWith(city))
                                     .LongCountAsync();

            /* var totalItems = await _eventCatalogContext.Events
                                     .Where(c => c.City.StartsWith(city))
                                     .LongCountAsync();*/
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
        [HttpGet]
        [Route("[action]/withcityname/{city:minlength(1)}")]
        public async Task<IActionResult> City(string city,
                       [FromQuery] int pageSize = 6,
                        [FromQuery] int pageIndex = 0)
        {
            var totalItems = await _eventCatalogContext.EventCities
                                 .Where(c => c.CityName.StartsWith(city))
                                 .LongCountAsync();
            var itemsOnPage = await _eventCatalogContext.EventCities
                                 .Where(c => c.CityName.StartsWith(city))
                                 .OrderBy(c => c.CityName)
                                 .Skip(pageSize * pageIndex)
                                 .Take(pageSize)
                                 .ToListAsync();
            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
            var model = new PaginatedEventViewModel<EventCity>
                 (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }
        //EventCity get methods - Bhuvana code end
        /*
        [HttpGet]
        [Route("[action]/withId/{cityId:int}/cityname/{cityName}")]
        public async Task<IActionResult> City
                     (int? cityId,string cityName,
                     [FromQuery] int pageSize = 6,
                     [FromQuery] int pageIndex = 0)
        {
                    var root = (IQueryable<EventCity>)_eventCatalogContext.EventCities;
                    if (cityId.HasValue)
                    {
                        root = root.Where(c => c.Id == cityId);
                    }
                    if(cityName != null)
                             root = root.Where(c => c.CityName.StartsWith(cityName));
                  
                    var totalItems = await root
                                        .LongCountAsync();
                    var itemsOnPage = await root
                                        .OrderBy(c => c.CityName)
                                        .Skip(pageSize * pageIndex)
                                        .Take(pageSize)
                                        .ToListAsync();
                    itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
                    var model = new PaginatedEventViewModel<EventCity>
                            (pageIndex, pageSize, totalItems, itemsOnPage);

                    return Ok(model);
        }
        //CityEvents
        [HttpGet]
        [Route("[action]/withId/{cityId:int}/cityname/{cityName}")]
        public async Task<IActionResult> CityEvents
                     (int? cityId, string cityName,
                     [FromQuery] int pageSize = 6,
                     [FromQuery] int pageIndex = 0)
        {

            var root = (IQueryable<Event>)_eventCatalogContext.Events;
            if (cityId.HasValue)
            {
                root = root.Where(c => c.Id == cityId);
            }
            if (cityName != null)
            {
                root = root.Where(c => c.City.StartsWith(cityName));
            }

            var totalItems = await root
                                .LongCountAsync();
            var itemsOnPage = await root
                                .OrderBy(c => c.Title)
                                .Skip(pageSize * pageIndex)
                                .Take(pageSize)
                                .ToListAsync();
            itemsOnPage = ChangeUrlPlaceHolder(itemsOnPage);
            var model = new PaginatedEventViewModel<Event>
                    (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }*/



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