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



        public async Task<IActionResult> Index(

            int? EventCategoryFilterApplied,

            int? EventTypeFilterApplied, int? page)
        {

            int itemsPage = 10;

            //get events from service who goes thr api path to get to eventcatalog api to get events from EventDB
            var ecatalog = await

                _ecatalogSvc.GetEvents

                (page ?? 0, itemsPage, EventCategoryFilterApplied,

                EventTypeFilterApplied);

            //pass events into view model to return back to httpclient
            var vm = new EventCatalogIndexViewModel()
            {

                Events = ecatalog.Data,

                EventCategories = await _ecatalogSvc.GetEventCategories(),

                EventTypes = await _ecatalogSvc.GetEventTypes(),

                EventCategoryFilterApplied = EventCategoryFilterApplied ?? 0,

                EventTypeFilterApplied = EventTypeFilterApplied ?? 0,

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

            return View(vm);

        }
       
    }
}
