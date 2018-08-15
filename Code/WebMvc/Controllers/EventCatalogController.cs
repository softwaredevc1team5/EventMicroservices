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

            //get eventcategories from service, then from apipath who gets it from EventCatalog api to get  categories from EventCategoryDB
            var ecategories = await _ecatalogSvc.GetEventCategoriesWithImage();

            //pass events, event and type in various ways  into view model to return back to httpclient
            var vm = new EventCatalogIndexViewModel()
            {

                Events = ecatalog.Data,

                EventCategories = await _ecatalogSvc.GetEventCategories(),

                EventCategoriesWithImage = ecategories.Data,

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


        public  IActionResult Search(string SearchEventTitle, string SearchEventCity)
        {
            //ViewData["Message"] = $"Your application description page. {SearchEventTitle}";

            if (SearchEventTitle != null)
            {
               
               
                if (SearchEventCity != null)
                {
                    //ViewData["Message"] = $"Searching by both title and city.";

                    //search both title and city
                    return RedirectToAction("CityTitle", "SearchEventCatalog", new { title = SearchEventTitle, city = SearchEventCity });
                }
                else
                {
                    //search for title
                    return RedirectToAction("Index", "SearchEventCatalog", new { title = SearchEventTitle });
                }

            }
            else
            {
                if (SearchEventCity != null)
                {
                    //search just city
                    // ViewData["Message"] = $"Searching by just city. {SearchEventCity}";
                    return RedirectToAction("Index", "CityDescription", new { city = SearchEventCity });
                }
                else
                {
                    //both title and city are empty
                    ViewData["Message"] = $"Enter event title and/or  city";
                }
            }

            
            return View();
        }

    }
}
