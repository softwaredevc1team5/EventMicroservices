using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class EventDetailController : Controller
    {
        private IEventCatalogService _ecatalogSvc;



        public EventDetailController(IEventCatalogService ecatalogSvc) =>

            _ecatalogSvc = ecatalogSvc;

        public async Task<IActionResult> EventDetail(int id)
        {
            //id = 1;
           
                var eventDetail = await _ecatalogSvc.GetEventItem(id);

                //pass event into view model to return back to httpclient
                var vm = new EventDetailViewModel
                {
                    Id = eventDetail.Id,
                    Title = eventDetail.Title,
                    Address = eventDetail.Address,
                    City = eventDetail.City,
                    State = eventDetail.State,
                    Zipcode = eventDetail.Zipcode,
                    ImageUrl = eventDetail.ImageUrl,
                    Price = eventDetail.Price,
                    StartDate = eventDetail.StartDate,
                    EndDate = eventDetail.EndDate,
                    EventTypeId = eventDetail.EventTypeId,
                    EventCategoryId = eventDetail.EventCategoryId,
                    EventType = eventDetail.EventType,
                    EventCategory = eventDetail.EventCategory,
                    OrganizerId = eventDetail.OrganizerId,
                    OrganizerName = eventDetail.OrganizerName
                };

                return View(vm);
            }
            
    }
}