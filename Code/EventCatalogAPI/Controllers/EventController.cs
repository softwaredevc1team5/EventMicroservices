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

        //GET api/events/title/Redmond/city/null/date/10/01/2018?pageSize=4&pageIndex=0
        [HttpGet]
        [Route("events/title/{title}/city/{city}/date/{date}")]
        public async Task<IActionResult> EventswithTitleCityDate(string title, string city, string date, [FromQuery] int pageSize = 6,
                                                                              [FromQuery] int pageIndex = 0)
        {
            var root = (IQueryable<Event>)_eventCatalogContext.Events;
            if (title !="notitle")
            {
                root = root.Where(c => c.Title.StartsWith(title));
            }
            if (city!="nocity")
            {
                root = root.Where(c => c.City.StartsWith(city));
            }
            if (date!="nodate")
            {
                DateTime dt = Convert.ToDateTime(date);
                root = root.Where(c => c.StartDate.ToShortDateString() == dt.ToShortDateString());
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

       //chitra
        [HttpGet]
        [Route("[action]/type/{eventTypeId}/category/{eventCategoryId}/date/{eventDate}/city/{eventCity}")]

        public async Task<IActionResult> EventsByFilters(int? eventTypeId, int? eventCategoryId, String eventDate, String eventCity, [FromQuery] int pageSize = 6,[FromQuery] int pageIndex = 0)
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
            if (eventCity != "null" && eventCity != "All")
            {
                var citystr = eventCity.Split(',')[0];
                var statestr = eventCity.Split(',')[1];
                root = root.Where(c => c.City == citystr && c.State == statestr);
            }

            if (eventDate != "null" && eventDate != "All Days")
            {
                root = FindingEventsByDate(root, eventDate);

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

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> AllEventsCities()
        {
            //IList<String> totalItems;
            List<String> cities = new List<string>();
            var totalItems = await _eventCatalogContext.Events.ToListAsync();
            foreach (var item in totalItems)
            {
                if (!cities.Contains(item.City + "," + item.State))
                    cities.Add(item.City + "," + item.State);
            }

            return Ok(cities);
        }

        public IQueryable<Event> FindingEventsByDate(IQueryable<Event> root, String date)
        {
            DateTime dateTime = DateTime.Now.Date;

            if (date != null && date != "All Days")
            {
                switch (date)
                {
                    case "Today":
                        dateTime = DateTime.Now;
                        root = root.Where(c => DateTime.Compare(c.StartDate.Date, dateTime.Date) == 0);
                        break;
                    case "Tomorrow":
                        var tomorrow = dateTime.AddDays(1);
                        root = root.Where(c => DateTime.Compare(c.StartDate.Date, tomorrow) == 0);
                        break;
                    case "This week":
                        var thisWeekdaySun = dateTime.AddDays(-(7 - (int)dateTime.DayOfWeek));
                        var comingSun = dateTime.AddDays(7 - (int)dateTime.DayOfWeek);

                        root = root.Where(c => (c.StartDate.Date >= thisWeekdaySun && c.StartDate.Date <= comingSun));

                        break;
                    case "This weekend":
                        var weekendFri = dateTime.AddDays(5 - (int)dateTime.DayOfWeek);
                        var weekendSun = dateTime.AddDays(7 - (int)dateTime.DayOfWeek);
                        root = root.Where(c => (c.StartDate.Date >= weekendFri && c.StartDate.Date <= weekendSun));
                        break;
                    case "Next week":
                        var nextWeekSunday = dateTime.AddDays((7 - (int)dateTime.DayOfWeek) + 7);
                        var comingSunday = dateTime.AddDays(7 - (int)dateTime.DayOfWeek);

                        root = root.Where(c => (c.StartDate.Date >= comingSunday && c.StartDate.Date <= nextWeekSunday));
                        break;
                    case "Next weekend":
                        var nextWeekFri = dateTime.AddDays(5 - (int)dateTime.DayOfWeek + 7);
                        var nextWeekSun = dateTime.AddDays(7 - (int)dateTime.DayOfWeek + 7);

                        root = root.Where(c => (c.StartDate.Date >= nextWeekFri && c.StartDate.Date <= nextWeekSun));

                        break;
                    case "This month":

                        var thisMonth = dateTime.Month;
                        root = root.Where(c => c.StartDate.Date.Month == thisMonth);
                        break;

                    case "Next month":
                        var nextMonth = dateTime.AddMonths(1).Month;
                        root = root.Where(c => c.StartDate.Date.Month == nextMonth);
                        break;
                    default:
                        break;

                }
            }

            return root;
        }


    }
}