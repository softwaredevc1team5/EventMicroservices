using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
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

                   string title, string city, string date, int? page)
        {


            int itemsPage = 10;
            
            var ecatalog = await

                _ecatalogSvc.GetEventsWithTitleCityDate(title, city, date, page ?? 0, itemsPage);


            //get eventcategories from service, then from apipath who gets it from EventCatalog api to get  categories from EventCategoryDB
            var ecategories = await _ecatalogSvc.GetEventCategoriesWithImage(page ?? 0, itemsPage);

            //pass events into view model to return back to httpclient
            var vm = new EventCatalogIndexViewModel()
            {

                Events = ecatalog.Data,

                EventCategories = await _ecatalogSvc.GetEventCategories(),

                EventCategoriesWithImage = ecategories.Data,

                EventTypes = await _ecatalogSvc.GetEventTypes(),

                EventCategoryFilterApplied = 0,

                EventTypeFilterApplied = 0,

                PaginationInfo = new PaginationInfo()

                {

                    ActualPage = page ?? 0,

                    ItemsPerPage = itemsPage, //catalog.Data.Count,

                    TotalItems = ecatalog.Count,

                    TotalPages = (int)Math.Ceiling(((decimal)ecatalog.Count / itemsPage))

                }

            };

            //update the categoryname of allevents in vm
            foreach (var category in vm.EventCategoriesWithImage)
            {
                foreach (var eventitem in vm.Events.Where(w => w.EventCategoryId == category.Id))
                {
                    eventitem.EventCategory = category.Name;
                }
            }

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";

            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";


            //heading to specify search
            if (title == "notitle")
            {
                title = "";
            }
            if (city == "nocity")
            {
                city = "";
            }
            if (date == "nodate")
            {
                date = "";
            }
            ViewData["Message"] = $"Search results for {title} {city} {date}";

            return View(vm);

        }

    }
}
