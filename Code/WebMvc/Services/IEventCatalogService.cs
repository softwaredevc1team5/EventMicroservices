using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.Services
{
    public interface IEventCatalogService

    {
       
       Task<EventCatalog> GetEvents(int page, int take, int? category, int? type);

       Task<EventCatalog> GetEventsWithTitle(string title, int page, int take);

       Task<EventCatalog> GetEventsWithTitleCityDate(string title, string city, string date, int page, int take);

       Task<IEnumerable<SelectListItem>> GetEventCategories();

       Task<EventCategoryCatalog> GetEventCategoriesWithImage(int page, int take);

       Task<IEnumerable<SelectListItem>> GetEventTypes();

        Task<Event> GetEventItem(int EventId);

        Task<EventCityCatalog> GetCityInfo(int? city);
        Task<EventCityCatalog> GetEventsInCity();
        Task<IEnumerable<SelectListItem>> GetCities();
    }
}
