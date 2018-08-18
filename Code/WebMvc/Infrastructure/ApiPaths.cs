using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public class ApiPaths
    {
        public static class EventCatalog

        {

            public static string GetAllEvents(string baseUri,

                int page, int take, int? category, int? type)

            {

                var filterQs = string.Empty;



                if (category.HasValue || type.HasValue)

                {

                    var eventcategoryQs = (category.HasValue) ? type.Value.ToString() : "null";

                    var eventtypeQs = (type.HasValue) ? type.Value.ToString() : "null";

                    filterQs = $"/eventtype/{eventtypeQs}/eventcategory/{eventcategoryQs}";

                }

               
                // return $"{baseUri}events{filterQs}";
               
               return $"{baseUri}events{filterQs}?pageSize={take}&pageIndex={page}";

            }



            public static string GetEvent(string baseUri, int id)

            {
                return $"{baseUri}events/{id}";

            }
            public static string GetEventsWithTitle(string baseUri, string title, int page, int take)

            {
             
                return $"{baseUri}events/withtitle/{title}?pageSize={take}&pageIndex={page}";
               // return $"{baseUri}events/withtitle/{title}";

            }
            public static string GetEventsWithTitleCityDate(string baseUri, string title, string city, string date, int page, int take)

            {

                return $"{baseUri}events/title/{title}/city/{city}/date/{date}?pageSize={take}&pageIndex={page}";
                
            }

            public static string GetAllEventCategories(string baseUri)

            {

                return $"{baseUri}eventCategories";

            }

            public static string GetAllEventCategoriesForImage(string baseUri, int page, int take)

            {
               
                return $"{baseUri}eventCategoriesforimage?pageSize={take}&pageIndex={page}";

            }



            public static string GetAllEventTypes(string baseUri)

            {

                return $"{baseUri}eventTypes";

            }
            //get all events by all filters by chitra
            public static string GetEventsByAllFilters(string baseUri,

                int page, int take, int? eventcategory, int? eventtype, String date, String city)

            {
                var filterQs = string.Empty;




                if (eventcategory.HasValue || eventtype.HasValue || city != null || date != null)

                {

                    var eventcategoryQs = (eventcategory.HasValue) ? eventcategory.Value.ToString() : "null";

                    var eventtypeQs = (eventtype.HasValue) ? eventtype.Value.ToString() : "null";
                    var eventdateQs = date ?? "null";
                    var eventcityQs = city ?? "null";
                    filterQs = $"/type/{eventtypeQs}/category/{eventcategoryQs}/date/{eventdateQs}/city/{eventcityQs}";
                }
                //filterQs = $"/date/{eventdateQs}/city/{eventcityQs}";
                //return $"{baseUri}events";
                return $"{baseUri}eventsByFilters{filterQs}?pageIndex={page}&pageSize={take}";
            }

            public static string GetAllCities(string baseUri)

            {
                return $"{baseUri}allEventsCities";
            }
        }
    }
}
