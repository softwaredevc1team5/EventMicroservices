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

                    var eventcategoryQs = (eventcategory.HasValue) ? eventcategory.Value.ToString() : "0";

                    var eventtypeQs = (eventtype.HasValue) ? eventtype.Value.ToString() : "0";

                    filterQs = $"/type/{eventtypeQs}/category/{eventcategoryQs}";

                }


                //return $"{baseUri}events";
               return $"{baseUri}events{filterQs}?pageIndex={page}&pageSize={take}";

            }



            public static string GetEvent(string baseUri, int id)

            {



                return $"{baseUri}/events/{id}";

            }

            public static string GetAllEventCategories(string baseUri)

            {

                return $"{baseUri}eventCategories";

            }



            public static string GetAllEventTypes(string baseUri)

            {

                return $"{baseUri}eventTypes";

            }

        }
    }
}
