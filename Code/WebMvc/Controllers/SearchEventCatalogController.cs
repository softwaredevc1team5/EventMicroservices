using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class SearchEventCatalogController: Controller
    {
        private IEventCatalogService _ecatalogSvc;



        public SearchEventCatalogController(IEventCatalogService ecatalogSvc) =>
             
            _ecatalogSvc = ecatalogSvc;



        public async Task<IActionResult> Index(

            string title, int? page)
        {



            //get events with title 
            //from service GetEventsWithTitle which calls GetEventsWithTitle in ApiPaths
            //which make api call for withtitle url

            int itemsPage = 10;

            var ecatalog = await

                _ecatalogSvc.GetEventsWithTitle(title, page ?? 0, itemsPage);

            //pass events into view model to return back to httpclient
            var vm = new EventCatalogIndexViewModel()
            {

                Events = ecatalog.Data,

                EventCategories = await _ecatalogSvc.GetEventCategories(),

                EventTypes = await _ecatalogSvc.GetEventTypes(),

                EventCategoryFilterApplied =  0,

                EventTypeFilterApplied =  0,

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
