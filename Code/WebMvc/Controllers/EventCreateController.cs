using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;
using WebMvc.Services;
using WebMvc.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMvc.Controllers
{
    public class EventCreateController : Controller
    {
        // GET: /<controller>/

        private IEventCatalogService _ecatalogSvc;

        public EventCreateController(IEventCatalogService ecatalogSvc) =>
                        _ecatalogSvc = ecatalogSvc;
        public IActionResult Index(EventCreate model)
        {
            
            return View(model);
        }
        //  [HttpPost]
        public IActionResult EventCreate(EventCreate model)
        {
            var vm = new EventCreateViewModel()
            {
                Event = model
            };
            // var citycatalog = await _ecatalogSvc.CreateEvent(model);
            return View(vm);
        }
    }
}
