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

        /*  public async Task<IActionResult> Index(string city, int? CityFilterApplied)
          {

              var eventcatalog = await _catalogSvc.GetCityInfo(city, CityFilterApplied);

              var vm = new EventCityIndexViewModel()
              {
                  CityItems = eventcatalog.Data,
                  CityFilterApplied = CityFilterApplied ?? 0,
                  Cities = await _catalogSvc.GetCities()
              };
              return View(vm);
          }*/
        public async Task<IActionResult> Index( string city)
         {
              var citycatalog = await _catalogSvc.GetCityInfo(city);
              var eventsCatalog = await _catalogSvc.GetEventsInCity(city);

             var vm = new EventCityIndexViewModel()
             {
                 CityItems = citycatalog.Data,
                 Events = eventsCatalog.Data,
                 CityFilterName = city,
                 Cities = await _catalogSvc.GetCities(),
                 PaginationInfo = new PaginationInfo()
                 {
                     ActualPage = 0,
                     ItemsPerPage = 6, //catalog.Data.Count,
                     TotalItems = eventsCatalog.Count,
                     TotalPages = (int)Math.Ceiling(((decimal)eventsCatalog.Count /6)),
                 }
             };
             if (vm.PaginationInfo.TotalItems < vm.PaginationInfo.ItemsPerPage)
                 vm.PaginationInfo.ItemsPerPage = vm.PaginationInfo.TotalItems;

             vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";

             vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

             return View(vm);
         }

        /* public async Task<IActionResult> CityFilter(int? CityFilterApplied,int? page)
         {
             int itemsPerPage = 6;
             var cityCatalog = await _catalogSvc.GetCityWithId(CityFilterApplied,page??0, itemsPerPage);
             var eventsCatalog = await _catalogSvc.GetEventsWithCityId(CityFilterApplied, page ?? 0, itemsPerPage);
             var vm = new EventCityIndexViewModel()
             {
                 CityItems = cityCatalog.Data,
                 Events = eventsCatalog.Data,
                 CityFilterApplied = CityFilterApplied,
                 Cities = await _catalogSvc.GetCities(),
                 PaginationInfo = new PaginationInfo()
                 {
                     ActualPage = 0,
                     ItemsPerPage = itemsPerPage, //catalog.Data.Count,
                     TotalItems = eventsCatalog.Count,
                     TotalPages = (int)Math.Ceiling(((decimal)eventsCatalog.Count / itemsPerPage)),
                 }
             };
             if (vm.PaginationInfo.TotalItems < vm.PaginationInfo.ItemsPerPage)
                 vm.PaginationInfo.ItemsPerPage = vm.PaginationInfo.TotalItems;

             vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";

             vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
             return View(vm);
         }*/
/*
        public async Task<IActionResult> Index(int? CityFilterApplied,string city,int? page)
        {
            int itemsPerPage = 6;
            var cityCatalog = await _catalogSvc.GetCityWithId(CityFilterApplied,city, page ?? 0, itemsPerPage);
            var eventsCatalog = await _catalogSvc.GetEventsWithCityId(CityFilterApplied,city, page ?? 0, itemsPerPage);
            var vm = new EventCityIndexViewModel()
            {
                CityItems = cityCatalog.Data,
                Events = eventsCatalog.Data,
                CityFilterApplied = CityFilterApplied??0,
                CityFilterName = city??null,
                Cities = await _catalogSvc.GetCities(),
                PaginationInfo = new PaginationInfo()
                {
                    ActualPage = 0,
                    ItemsPerPage = itemsPerPage, //catalog.Data.Count,
                    TotalItems = eventsCatalog.Count,
                    TotalPages = (int)Math.Ceiling(((decimal)eventsCatalog.Count / itemsPerPage)),
                }
            };
            if (vm.PaginationInfo.TotalItems < vm.PaginationInfo.ItemsPerPage)
                vm.PaginationInfo.ItemsPerPage = vm.PaginationInfo.TotalItems;

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";

            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
            return View(vm);
        }*/
    }
}
