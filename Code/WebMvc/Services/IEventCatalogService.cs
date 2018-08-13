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
       
       Task<EventCatalog> GetEvents(int page, int take, int? brand, int? type);

       Task<IEnumerable<SelectListItem>> GetEventCategories();

        Task<IEnumerable<SelectListItem>> GetEventTypes();
    }
}
