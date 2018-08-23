using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Infrastructure;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class EventCatalogController: Controller
    {
        private IEventCatalogService _ecatalogSvc;

        public EventCatalogController(IEventCatalogService ecatalogSvc) =>
             
            _ecatalogSvc = ecatalogSvc;
        // starting Index of app
        public async Task<IActionResult> Index(

            int? EventCategoryFilterApplied,

            int? EventTypeFilterApplied, int? page, String EventDateFilterApplied)
        {

            int itemsPage = 9;

            

            //get events from service who goes thr api path to get to eventcatalog api to get events from EventDB
            var ecatalog = await

                _ecatalogSvc.GetEvents

                (page ?? 0, itemsPage, EventCategoryFilterApplied,

                EventTypeFilterApplied);

            //get eventcategories from service, then from apipath who gets it from EventCatalog api to get  categories from EventCategoryDB
            var ecategories = await _ecatalogSvc.GetEventCategoriesWithImage(page ?? 0, itemsPage);

            //pass events, event and type in various ways  into view model to return back to httpclient
            var vm = new EventCatalogIndexViewModel()
            {

                Events = ecatalog.Data,
                EventDates = _ecatalogSvc.GetEventDates(),
                EventDateFilterApplied = EventDateFilterApplied,

                EventCategories = await _ecatalogSvc.GetEventCategories(),

                EventCategoriesWithImage = ecategories.Data,

                EventTypes = await _ecatalogSvc.GetEventTypes(),

                EventCategoryFilterApplied = EventCategoryFilterApplied ?? 0,

                EventTypeFilterApplied = EventTypeFilterApplied ?? 0,
   
                PaginationInfo = new PaginationInfo()
                {
                    ActualPage = page ?? 0,

                    TotalItems = ecatalog.Count,
                    ItemsPerPage = ecatalog.Count < itemsPage ? ecatalog.Count : itemsPage, //catalog.Data.Count,


                    TotalPages = (int)Math.Ceiling(((decimal)ecatalog.Count / itemsPage))
                }
            };

            //update the categoryname of allevents in vm
            foreach (var category in vm.EventCategoriesWithImage) {
                foreach (var eventitem in vm.Events.Where(w => w.EventCategoryId == category.Id))
                {
                    eventitem.EventCategory = category.Name;
                }
            }

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return View(vm);

        }
        public   IActionResult Search(string SearchEventTitle, string SearchEventCity, string SearchEventDate)
        {
            //var SearchEventDate = EventDateFilterApplied;
            //ViewData["Message"] = $"Your application description page. {SearchEventTitle}";
            //DateTime SearchEventDate = DateTime.Parse(strSearchEventDate,  MM-dd-yyyy);

            if (SearchEventTitle == null && SearchEventDate == null && SearchEventCity != null)
            {
                //bhuvana
                return RedirectToAction("Index", "EventCity", new { city = SearchEventCity });
            }
            else if (SearchEventTitle == null && SearchEventDate == null && SearchEventCity == null)
            {
                //uer did not provide anything
                ViewData["Message"] = $"PLEASE ENTER TITLE OR CITY OR DATE";
            }
            else if(SearchEventTitle != null || SearchEventDate != null && SearchEventCity != null)
            {
                //IEnumerable<SelectListItem> ListOfCities = Convert.ChangeType(_ecatalogSvc.GetAllCities())_ecatalogSvc.GetAllCities();
              
                //string text = ListOfCities.Where(x => x.Text.ToLower() == SearchEventCity.ToLower() ).FirstOrDefault().Text;
                return RedirectToAction("EventSearchByCategory", "EventCatalog", new { EventCityFilterApplied = SearchEventCity, EventDateFilterApplied = SearchEventDate });
            }
            else
            {
                //allother redirect to search
                if(SearchEventTitle == null)
                {
                    SearchEventTitle = "notitle";
                }
                if (SearchEventCity == null)
                {
                    SearchEventCity = "nocity";
                }
                if (SearchEventDate == null || SearchEventDate == "mm-dd-yyyy")
                {
                    SearchEventDate = "nodate";
                }
                return RedirectToAction("Index", "SearchEventCatalog", new { title = SearchEventTitle, city = SearchEventCity, date = SearchEventDate });

            }


            return View();
        }

        //When clicking on category
        public async Task<IActionResult> EventSearchByCategory(int? EventCategoryFilterApplied, int? EventTypeFilterApplied, int? page, String EventDateFilterApplied, String EventCityFilterApplied)
        {

            int itemsPage = 9;
            var ecatalog = await _ecatalogSvc.GetEventsByAllFilters(page ?? 0, itemsPage, EventCategoryFilterApplied, EventTypeFilterApplied, EventDateFilterApplied, EventCityFilterApplied);

            var vm = new EventFiltersCatalogViewModel()
            {

                Events = ecatalog.Data,

                AllCities = await _ecatalogSvc.GetAllCities(),
                EventCategories = await _ecatalogSvc.GetEventCategories(),

                EventTypes = await _ecatalogSvc.GetEventTypes(),
                EventDates = _ecatalogSvc.GetEventDates(),

                EventCategoryFilterApplied = EventCategoryFilterApplied ?? 0,

                EventTypeFilterApplied = EventTypeFilterApplied ?? 0,
                EventDateFilterApplied = EventDateFilterApplied ?? "null",
                EventCityFilterApplied = EventCityFilterApplied ?? "null",
                PaginationInfo = new PaginationInfo()

                {

                    ActualPage = page ?? 0,

                    TotalItems = ecatalog.Count,
                    ItemsPerPage = ecatalog.Count < itemsPage ? ecatalog.Count : itemsPage, //catalog.Data.Count,

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
