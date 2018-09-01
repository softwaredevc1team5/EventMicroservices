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
        //chitra
        Task<EventCatalog> GetEventsByAllFilters(int page, int take, int? brand, int? type, String date, String city);
        Task<IEnumerable<SelectListItem>> GetAllCities();
        IEnumerable<SelectListItem> GetEventDates();

        Task<EventCatalog> GetEvents(int page, int take, int? category, int? type);

       Task<EventCatalog> GetEventsWithTitle(string title, int page, int take);

       Task<EventCatalog> GetEventsWithTitleCityDate(string title, string city, string date, int page, int take);

       Task<IEnumerable<SelectListItem>> GetEventCategories();

       Task<EventCategoryCatalog> GetEventCategoriesWithImage(int page, int take);

        Task<List<EventCategory>> GetEventCategoriesForHashTag();

       Task<IEnumerable<SelectListItem>> GetEventTypes();
        
        Task<Event> GetEventItem(int EventId);

        //EventCities
        Task<EventCityCatalog> GetCityInfo(string city);
        Task<EventCatalog> GetEventsInCity(string city);
       // Task<EventCityCatalog> GetCityWithId(int? cityFilterApplied,string city,int page,int take);
        //Task<EventCatalog> GetEventsWithCityId(int? cityFilterApplied,string city, int page, int take);
        Task<IEnumerable<SelectListItem>> GetCities();

        Task<int> CreateEvent(EventForCreation newEvent);
    }
}
