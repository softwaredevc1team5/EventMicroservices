using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMvc.Models;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class EventCityController : Controller
    {
       
        private IEventCatalogService _catalogSvc;

        public EventCityController(IEventCatalogService catalogSvc) =>
            _catalogSvc = catalogSvc;
        
        public async Task<IActionResult> Index(int? cityFilterApplied)
        {
            // int eventsPage = 10;
             var eventcatalog = await _catalogSvc.GetCityInfo(cityFilterApplied);
           
            var vm = new CityIndexViewModel()
            {
              //var eventcatalog = await _catalogSvc.GetCityInfo(CityFilterApplied),
                CityItems =eventcatalog.Data,
                CityFilterApplied = cityFilterApplied ?? 0,
                Cities = await _catalogSvc.GetCities()
            };
            return View(vm);
        }

    }
}
