using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Infrastructure;
using WebMvc.Models;
using WebMvc.Services;

namespace WebMvc.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;

        private readonly IHttpClient _apiClient;

        private readonly string _remoteServiceBaseUrl;


        public EventCatalogService(IOptionsSnapshot<AppSettings> settings,

            IHttpClient httpClient)

        {

            _settings = settings;

            _apiClient = httpClient;

               _remoteServiceBaseUrl = $"{_settings.Value.EventCatalogUrl}/api/event/";
       
        }

        public async Task<IEnumerable<SelectListItem>> GetEventCategories()
        {
            
            var getEventCategoriesUri = ApiPaths.EventCatalog.GetAllEventCategories(_remoteServiceBaseUrl);

            var dataString = await _apiClient.GetStringAsync(getEventCategoriesUri);



            var items = new List<SelectListItem>

            {

                new SelectListItem() { Value = null, Text = "All", Selected = true }

            };

            var categories = JArray.Parse(dataString);

            foreach (var category in categories.Children<JObject>())

            {

                items.Add(new SelectListItem()

                {

                    Value = category.Value<string>("id"),

                    Text = category.Value<string>("name")

                });

            }



            return items;

        }

        public async Task<EventCategoryCatalog> GetEventCategoriesWithImage(int page, int take)
        {
            var getEventCategoriesUri = ApiPaths.EventCatalog.GetAllEventCategoriesForImage(_remoteServiceBaseUrl, page, take);

            var dataString = await _apiClient.GetStringAsync(getEventCategoriesUri);          

            var response = JsonConvert.DeserializeObject<EventCategoryCatalog>(dataString);

            return response;
        }


        public async Task<EventCatalog> GetEvents(int page, int take, int? category, int? type)

        {

            var alleventsUri = ApiPaths.EventCatalog.GetAllEvents(_remoteServiceBaseUrl, page, take, category, type);

            var dataString = await _apiClient.GetStringAsync(alleventsUri);

            var response = JsonConvert.DeserializeObject<EventCatalog>(dataString);

            return response;

        }

        public async Task<EventCatalog> GetEventsWithTitle(string title, int page, int take)

        {

            var eventswithtitleUri = ApiPaths.EventCatalog.GetEventsWithTitle(_remoteServiceBaseUrl,title,page, take);



            var dataString = await _apiClient.GetStringAsync(eventswithtitleUri);



            var response = JsonConvert.DeserializeObject<EventCatalog>(dataString);



            return response;

        }

        public async Task<EventCatalog> GetEventsWithTitleCityDate(string title, string city, string date, int page, int take)

        {

            var eventswithtitleUri = ApiPaths.EventCatalog.GetEventsWithTitleCityDate(_remoteServiceBaseUrl, title, city, date, page, take);



            var dataString = await _apiClient.GetStringAsync(eventswithtitleUri);



            var response = JsonConvert.DeserializeObject<EventCatalog>(dataString);



            return response;

        }

        public async Task<Event> GetEventItem(int EventId)
        {
            var getEventDetailUri = ApiPaths.EventCatalog.GetEvent(_remoteServiceBaseUrl, EventId);

            var dataString = await _apiClient.GetStringAsync(getEventDetailUri);

            var item = JsonConvert.DeserializeObject<Event>(dataString);

            return item;
        }

        public async Task<IEnumerable<SelectListItem>> GetEventTypes()

        {

            var getEventTypesUri = ApiPaths.EventCatalog.GetAllEventTypes(_remoteServiceBaseUrl);



            var dataString = await _apiClient.GetStringAsync(getEventTypesUri);



            var items = new List<SelectListItem>

            {

                new SelectListItem() { Value = null, Text = "All", Selected = true }

            };

            var types = JArray.Parse(dataString);

            foreach (var type in types.Children<JObject>())

            {

                items.Add(new SelectListItem()

                {

                    Value = type.Value<string>("id"),

                    Text = type.Value<string>("type")

                });

            }

            return items;

        }

       public async Task<EventCatalog> GetEventsByAllFilters(int page, int take, int? category, int? type, String date, String city)
        {
            var alleventsUri = ApiPaths.EventCatalog.GetEventsByAllFilters(_remoteServiceBaseUrl,  page, take,  category,  type, date, city);


            var dataString = await _apiClient.GetStringAsync(alleventsUri);




            var response = JsonConvert.DeserializeObject<EventCatalog>(dataString);



            return response;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllCities()
        {

            var getAllCitiesUri = ApiPaths.EventCatalog.GetAllCities(_remoteServiceBaseUrl);


            var dataString = await _apiClient.GetStringAsync(getAllCitiesUri);



            var items = new List<SelectListItem>

            {

                new SelectListItem() { Value = null, Text = "All", Selected = true }

            };
            var cities = JsonConvert.DeserializeObject<List<string>>(dataString);
            //var cities = JArray.Parse(dataString);
            foreach (var city in cities)
            {
                items.Add(new SelectListItem()
                {
                    Text = city,
                    Value = city
                });
            }
            return items;
        }

        public IEnumerable<SelectListItem> GetEventDates()
        {
            List<String> eventDates = new List<string> { "Today", "Tomorrow","This week", "This weekend", "Next week", "Next weekend", "This month", "Next month"};
            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = "All Days", Text = "All Days", Selected = true }
            };
            foreach (var eventDate in eventDates)
            {
                items.Add(new SelectListItem()
                {
                    Value = eventDate,

                    Text = eventDate
                });
            }
            return items;
        }

    }
}
