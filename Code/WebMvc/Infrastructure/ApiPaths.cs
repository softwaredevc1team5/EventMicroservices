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
            //EventCity ApiPaths
            public static string GetCatalogEventsInCity(string baseUri, string city)
            {
                return $"{baseUri}Events/withcity/{city}?pageSize=6&pageIndex=0";
            }
            public static string GetCityDescription(string baseUri, string city)
            {
                return $"{baseUri}City/withcityname/{city}?pageSize=6&pageIndex=0";
            }
            public static string GetAllCities(string baseUri)
            {
                return $"{baseUri}EventCities";

            }

            //Get city with cityId or cityName
            /*  public static string GetCityDescription(string baseUri,int? cityFilterApplied,string city,int page,int take)
              {
                  var filterQs = string.Empty;

                  if (cityFilterApplied.HasValue || city != null)
                  {
                     var cityFilterQs = (cityFilterApplied.HasValue) ? cityFilterApplied.Value.ToString() : "null";

                      var cityQs =   city??"null";

                      filterQs = $"City/withId/{cityFilterQs}/cityname/{cityQs}";

                  }
                  return $"{baseUri}{filterQs}?pageSize={take}&pageIndex={page}";
                 // return $"{baseUri}City/withId/{cityFilterApplied}/cityname/{city}?pageSize={take}&pageIndex={page}";
              }
              //Get cityEvents with cityId or cityName
              //[action]/withId/{cityId:int}/cityname/{cityName}
              public static string GetEventsWithCityId(string baseUri, int? cityFilterApplied,string city, int page, int take)
              {
                  return $"{baseUri}CityEvents/withId/{cityFilterApplied}/cityname/{city}?pageSize={take}&pageIndex={page}";
              }
              */


        }
    }
}
