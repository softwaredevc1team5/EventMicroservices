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

               
                 return $"{baseUri}events{filterQs}";
               
              // return $"{baseUri}events{filterQs}?pageSize={page}&pageIndex={take}";

            }



            public static string GetEvent(string baseUri, int id)

            {
                return $"{baseUri}events/{id}";

            }
            public static string GetEventsWithTitle(string baseUri, string title, int page, int take)

            {
             
               // return $"{baseUri}events/withtitle/{title}?pageSize={page}&pageIndex={take}";
                return $"{baseUri}events/withtitle/{title}";

            }

            public static string GetAllEventCategories(string baseUri)

            {

                return $"{baseUri}eventCategories";

            }

            public static string GetAllEventCategoriesForImage(string baseUri)

            {

                return $"{baseUri}eventCategoriesforimage";

            }



            public static string GetAllEventTypes(string baseUri)

            {

                return $"{baseUri}eventTypes";

            }
            //EventCity ApiPaths
            public static string GetCatalogEventsInCity(string baseUri, string city)
            {
                return $"{baseUri}EventCities";
            }
            public static string GetCityDescription(string baseUri, int? city)
            {
                var filterQs = string.Empty;
                if (city.HasValue)
                {
                    filterQs = (city.HasValue) ? city.Value.ToString() : "null";
                }
                return $"{baseUri}City/withId/{1}?pageSize=1&pageIndex=0";
                // }
                // return $"{baseUri}City/withId/{filterQs}?pageSize={6}&pageIndex={0}";
                //return $"{baseUri}City/withname/{city}";
            }
            public static string GetAllCities(string baseUri)
            {
                return $"{baseUri}EventCities";

            }

        }
    }
}
