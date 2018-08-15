using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class EventCatalogIndexViewModel
    {
        public IEnumerable<Event> Events { get; set; }

        public IEnumerable<SelectListItem> EventCategories { get; set; }

        public IEnumerable<EventCategory> EventCategoriesWithImage { get; set; }

        public IEnumerable<SelectListItem> EventTypes { get; set; }

        public int? EventCategoryFilterApplied { get; set; }

        public int? EventTypeFilterApplied { get; set; }

        public PaginationInfo PaginationInfo { get; set; }

       // public PaginationInfo PaginationInfo1 { get; set; }
    }
}
