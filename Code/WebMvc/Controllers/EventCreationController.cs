
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WebMvc.Models;
using WebMvc.Services;
using WebMvc.ViewModels;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMvc.Controllers
{
    [Authorize]
    public class EventCreationController : Controller
    {
        // GET: /<controller>/

        private IEventCatalogService _ecatalogSvc;
       // private IBus _bus;

        public EventCreationController(IEventCatalogService ecatalogSvc)
        {
            _ecatalogSvc = ecatalogSvc;
           

        }

        public async Task<IActionResult> Index(int? EventCategoryFilterApplied, int? EventTypeFilterApplied)
        {
            var vm = new EventCreationViewModel()
            {
                Event = new EventForCreation(),
                // Event = new Event(),
                EventTypeFilterApplied = EventTypeFilterApplied ?? 0,
                EventTypes = await _ecatalogSvc.GetEventTypes(),

                EventCategoryFilterApplied = EventCategoryFilterApplied ?? 0,
                EventCategories = await _ecatalogSvc.GetEventCategories()
            };
            return View(vm);
        }
        //Direct Called to EventApi
        [HttpPost]
        public async Task<IActionResult> Create(EventCreationViewModel frmEvent)
        {
            if (ModelState.IsValid)
            {
                frmEvent.Event.EventTypeId = frmEvent.EventTypeFilterApplied ?? 0;
                frmEvent.Event.EventCategoryId = frmEvent.EventCategoryFilterApplied ?? 0;
                var eventId = await _ecatalogSvc.CreateEvent(frmEvent.Event);
           
                frmEvent.Event.Id = eventId;
                var vm = new EventCreationViewModel()
                {
                    Event = frmEvent.Event,
                    EventTypeFilterApplied = frmEvent.EventTypeFilterApplied,
                    EventTypes = await _ecatalogSvc.GetEventTypes()
                };

              //  var eventType = frmEvent.EventTypes.GetType();
                //return View(vm);
                // return RedirectToAction("EventSaved", new { id = eventId, userName = "UserName" });

                return RedirectToAction("EventSaved", frmEvent.Event);
                
            }
            return View(frmEvent);

        }






        public IActionResult EventCreate()
        {
            return View();

        }

        public IActionResult EventSaved(EventForCreation eventForCreation)
        {
            return View(eventForCreation);

        }


    }
}
