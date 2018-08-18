using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class EventFiltersCatalogViewModel
    {
        public IEnumerable<Event> Events { get; set; }

        public IEnumerable<SelectListItem> EventCategories { get; set; }

        public IEnumerable<SelectListItem> EventTypes { get; set; }
        public IEnumerable<SelectListItem> AllCities { get; set; }
        public IEnumerable<SelectListItem> EventDates { get; set; }


        public String EventDateFilterApplied { get; set; }
        public String EventCityFilterApplied { get; set; }

        public int? EventCategoryFilterApplied { get; set; }

        public int? EventTypeFilterApplied { get; set; }

        public PaginationInfo PaginationInfo { get; set; }
    }
}
