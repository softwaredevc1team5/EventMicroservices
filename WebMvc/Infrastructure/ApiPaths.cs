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

                int page, int take, int? eventcategory, int? eventtype, String date, String city)

            {

                var filterQs = string.Empty;



                if (eventcategory.HasValue || eventtype.HasValue || city != null ||date!= null)

                {

                    var eventcategoryQs = (eventcategory.HasValue) ? eventcategory.Value.ToString() : "null";

                    var eventtypeQs = (eventtype.HasValue) ? eventtype.Value.ToString() : "null";
                    var eventdateQs = date ?? "null";
                    var eventcityQs = city ?? "null";
                    filterQs = $"/type/{eventtypeQs}/category/{eventcategoryQs}/date/{eventdateQs}/city/{eventcityQs}";
                }
                
               

                //filterQs = $"/date/{eventdateQs}/city/{eventcityQs}";



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

            public static string GetAllCities(string baseUri)

            {


                return $"{baseUri}allEventsCities";

            }

        }
    }
}
