
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using WebMvc.Models;
using WebMvc.Services;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMvc.Controllers
{
    public class EventCreationController : Controller
    {
        // GET: /<controller>/

        private IEventCatalogService _ecatalogSvc;
       // private IBus _bus;

        public EventCreationController(IEventCatalogService ecatalogSvc)
        {
            _ecatalogSvc = ecatalogSvc;
           

        }
       
                        


        public IActionResult Index(EventForCreation model)
        {
            
            return View(model);
        }

        //Direct Called to EventApi
        [HttpPost]
        public async Task<IActionResult> Create(EventForCreation frmEvent)
        {
            if (ModelState.IsValid)
            {
                //  var user = _identitySvc.Get(HttpContext.User);
                EventForCreation eve = frmEvent;
                eve.EventCategoryId = 1; //Change so you get the data fron the Interface
                eve.EventTypeId =1;//Change so you get the data fron the Interface
                eve.OrganizerId = 2; 
                var eventId = await _ecatalogSvc.CreateEvent(eve);

                return RedirectToAction("EventSaved", new { id = eventId, userName = "UserName" });
            }
            else
            {
                return View(frmEvent);
            }
        }


    
        


        public IActionResult EventCreate()
        {
            return View();

        }

        public IActionResult EventSaved(int id, string userName)
        {         
            return View(id);

        }


    }
}
