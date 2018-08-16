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

                int page, int take, int? eventcategory, int? eventtype)

            {

                var filterQs = string.Empty;



                if (eventcategory.HasValue || eventtype.HasValue)

                {

                    var eventcategoryQs = (eventcategory.HasValue) ? eventcategory.Value.ToString() : "null";

                    var eventtypeQs = (eventtype.HasValue) ? eventtype.Value.ToString() : "null";

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

        }
    }
}
