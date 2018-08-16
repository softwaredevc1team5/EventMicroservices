using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class EventCatalogController: Controller
    {
        private IEventCatalogService _ecatalogSvc;



        public EventCatalogController(IEventCatalogService ecatalogSvc) =>
             
            _ecatalogSvc = ecatalogSvc;

        public  async Task<IActionResult> EventSearch(int? EventCategoryFilterApplied, int?  EventTypeFilterApplied, int? page, String EventDateFilterApplied, String EventCityFilterApplied)
        {
            int itemsPage = 10;
            var ecatalog = await  _ecatalogSvc.GetEvents(page ?? 0, itemsPage, EventCategoryFilterApplied, EventTypeFilterApplied, EventDateFilterApplied,EventCityFilterApplied);

            var vm = new EventCatalogIndexViewModel()
            {

                Events = ecatalog.Data,

                AllCities = await _ecatalogSvc.GetAllCities(),
                EventCategories = await _ecatalogSvc.GetEventCategories(),

                EventTypes = await _ecatalogSvc.GetEventTypes(),
                EventDates = _ecatalogSvc.GetEventDates(),

                EventCategoryFilterApplied = EventCategoryFilterApplied ?? 0,

                EventTypeFilterApplied = EventTypeFilterApplied ?? 0,
                EventDateFilterApplied = EventDateFilterApplied??"null" ,
                EventCityFilterApplied = EventCityFilterApplied??"null",
                PaginationInfo = new PaginationInfo()

                {

                    ActualPage = page ?? 0,

                    ItemsPerPage = itemsPage, //catalog.Data.Count,

                    TotalItems = ecatalog.Count,

                    TotalPages = (int)Math.Ceiling(((decimal)ecatalog.Count / itemsPage))

                }

            };



            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";

            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
            //RedirectToAction("Index", "Catalog");
            return View(vm);
        }

        public async Task<IActionResult> Index(int? EventCategoryFilterApplied, int? EventTypeFilterApplied, int? page, String EventDateFilterApplied, String EventCityFilterApplied)
        {
            int itemsPage = 10;
            var ecatalog = await _ecatalogSvc.GetEvents(page ?? 0, itemsPage, EventCategoryFilterApplied, EventTypeFilterApplied, EventDateFilterApplied, EventCityFilterApplied);

            var vm = new EventCatalogIndexViewModel()
            {

                Events = ecatalog.Data,

                EventDateFilterApplied = "Today",
                EventCityFilterApplied = EventCityFilterApplied ?? "null",

                AllCities = await _ecatalogSvc.GetAllCities(),
                EventCategories = await _ecatalogSvc.GetEventCategories(),

                EventTypes = await _ecatalogSvc.GetEventTypes(),
                EventDates = _ecatalogSvc.GetEventDates(),

                EventCategoryFilterApplied = EventCategoryFilterApplied ?? 0,

                EventTypeFilterApplied = EventTypeFilterApplied ?? 0,
               // EventDateFilterApplied = "Today",
                //EventCityFilterApplied = EventCityFilterApplied?? "null",
                PaginationInfo = new PaginationInfo()

                {

                    ActualPage = page ?? 0,

                    ItemsPerPage = itemsPage, //catalog.Data.Count,

                    TotalItems = ecatalog.Count,

                    TotalPages = (int)Math.Ceiling(((decimal)ecatalog.Count / itemsPage))

                }

            };



            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";

            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
            //RedirectToAction("Index", "Catalog");
            return View(vm);
        }

    }
}
