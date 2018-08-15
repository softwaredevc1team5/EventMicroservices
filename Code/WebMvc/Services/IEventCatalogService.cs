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

       Task<IEnumerable<SelectListItem>> GetEventCategories();

       Task<EventCategoryCatalog> GetEventCategoriesWithImage();

       Task<IEnumerable<SelectListItem>> GetEventTypes();

        Task<Event> GetEventItem(int EventId);
    }
}
