using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class EventCityIndexViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<EventCity> CityItems { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public string CityFilterApplied { get; set; }
        public string CityFilterName { get; set; }
      //  public string? SelectedCityFilter { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
