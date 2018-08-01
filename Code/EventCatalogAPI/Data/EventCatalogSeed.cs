using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Data
{
    public class EventCatalogSeed
    {
        public static async Task SeedAsync(EventCatalogContext context)
        {
            context.Database.Migrate();

            if (!context.EventCategories.Any())
            {
                context.EventCategories.AddRange
                    (GetPreconfiguredEventCategories());

                await context.SaveChangesAsync();
            }

            if (!context.EventTypes.Any())
            {
                context.EventTypes.AddRange
                    (GetPreconfiguredEventTypes());
                context.SaveChanges();
            }

            if (!context.Events.Any())
            {
                context.Events.AddRange
                    (GetPreconfiguredEvents());
                context.SaveChanges();
            }

        }



        static IEnumerable<EventCategory> GetPreconfiguredEventCategories()
        {
            return new List<EventCategory>()
            {

                new EventCategory() { Name = "Music"},
                new EventCategory() { Name = "Arts"},
                new EventCategory() { Name = "Food and Drink"},
                new EventCategory() { Name = "Business"},
                new EventCategory() { Name = "Health"},
                new EventCategory() { Name = "Charity"},
                new EventCategory() { Name = "Science and Tech"},
                new EventCategory() { Name = "Fashion"},
                new EventCategory() { Name = "Film and Media"},
                new EventCategory() { Name = "Other"}


            };

        }



        static IEnumerable<EventType> GetPreconfiguredEventTypes()
        {

            return new List<EventType>()
            {

                new EventType() { Type = "Class"},
                new EventType() { Type = "Seminar"},
                new EventType() { Type = "Performance" },
                new EventType() { Type = "Festival" },
                new EventType() { Type = "Gala" },
                new EventType() { Type = "Expo" },
                new EventType() { Type = "Conference" },
                new EventType() { Type = "Screening" },
                new EventType() { Type = "Networking" },
                new EventType() { Type = "Other" }

            };

        }



        static IEnumerable<Event> GetPreconfiguredEvents()

        {

            return new List<Event>()
            {

                new Event() {EventTypeId=1,EventCategoryId=3, Title = "A Night with Katy Perry",  OrganizerId = 1, Address= "MerryWeather Pavilion", City = "Baltimore", State ="Maryland", Zipcode ="21105", Price = 49.5M, StartDate = new DateTime(2018, 8, 10, 7, 10, 0), EndDate = new DateTime(2018, 10, 10, 9, 15, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1" },
                new Event() {EventTypeId=6,EventCategoryId=5, Title = "Feed the Children Gala", OrganizerId = 2, Address= "222 Observation Way", City = "New York City", State ="New York", Zipcode ="10103", Price = 500.0M, StartDate = new DateTime(2018, 4, 16, 20, 30, 0), EndDate = new DateTime(2018, 4, 16, 23, 0, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2" },
                new Event() {EventTypeId=7,EventCategoryId=1, Title = "Learn Python for Free", OrganizerId = 3, Address= "150 Tech Circle", City = "Redmond", State ="Washington", Zipcode ="98117", Price = 0.0M, StartDate = new DateTime(2018, 3, 6, 12, 0, 0), EndDate = new DateTime(2018, 3, 8, 12, 0, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3" },
                new Event() {EventTypeId=3,EventCategoryId=4, Title = "Burger Fest", OrganizerId = 4, Address= "237 Long Horn Drive", City = "Dallas", State ="Texas", Zipcode ="78110", Price = 0.0M, StartDate = new DateTime(2018, 9, 27, 9, 0, 0), EndDate = new DateTime(2018, 9, 27, 17, 30, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4" },
                new Event() {EventTypeId=2,EventCategoryId=3, Title = "Death of a Salesman", OrganizerId = 5, Address= "Washington State Theater", City = "Bellvue", State ="Washington", Zipcode ="98006", Price = 34.99M, StartDate = new DateTime(2018, 9, 23, 20, 0, 0), EndDate = new DateTime(2018, 9, 23, 22, 0, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5" },
                new Event() {EventTypeId=5,EventCategoryId=7, Title = "Tropical Epedimelogy 2018", OrganizerId = 6, Address= "349 Hospital Blvd", City = "Redmond", State ="Washington", Zipcode ="98118", Price = 300.00M, StartDate = new DateTime(2018, 7, 5, 0, 0, 0), EndDate = new DateTime(2018, 7, 9, 0, 0, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6" },
                new Event() {EventTypeId=10,EventCategoryId=10, Title = "Professional Singles Meetup", OrganizerId = 3, Address= "90 Cherish Avenue", City = "Frederick", State ="Maryland", Zipcode ="21702", Price = 10.00M, StartDate = new DateTime(2018, 2, 1, 12, 0, 0), EndDate = new DateTime(2018, 2, 2, 14, 0, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7" },
                new Event() {EventTypeId=7,EventCategoryId=2, Title = "Blockchains's Future in  WallStreet", OrganizerId = 4, Address= "101 Bull Street", City = "New York City", State ="New York", Zipcode ="10105", Price = 0.00M, StartDate = new DateTime(2018, 1, 10, 11, 0, 0), EndDate = new DateTime(2018, 1, 10, 12, 0, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },

                new Event() {EventTypeId=1,EventCategoryId=3, Title = "Janet Jackson Live",  OrganizerId = 7, Address= "Radio City Music Hall", City = "New York City", State ="New York", Zipcode ="10105", Price = 75.5M, StartDate = new DateTime(2018, 11, 10, 7, 10, 0), EndDate = new DateTime(2018, 11, 10, 9, 15, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/11" },
                new Event() {EventTypeId=6,EventCategoryId=5, Title = "Whales Protection Fund", OrganizerId = 6, Address= " 337 Fancy Pants Street", City = "Dallas", State ="Texas", Zipcode ="79231", Price = 1000.0M, StartDate = new DateTime(2018, 4, 16, 20, 30, 0), EndDate = new DateTime(2018, 4, 16, 23, 0, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/12" },
                new Event() {EventTypeId=7,EventCategoryId=1, Title = "Disney on Ice", OrganizerId = 1, Address= "MerryWeather Pavilion", City = "Baltimore", State ="Maryland", Zipcode ="21105", Price = 30.0M, StartDate = new DateTime(2018, 3, 6, 12, 0, 0), EndDate = new DateTime(2018, 3, 8, 12, 0, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/13" },
                new Event() {EventTypeId=3,EventCategoryId=4, Title = "Free Wine Tasting ", OrganizerId = 2, Address= " 87 Picolo Circle", City = "Bellevue", State ="Washington", Zipcode ="98004", Price = 0.0M, StartDate = new DateTime(2018, 9, 27, 9, 0, 0), EndDate = new DateTime(2018, 9, 27, 17, 30, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/10" },
                new Event() {EventTypeId=2,EventCategoryId=3, Title = "Macbeth", OrganizerId = 10, Address= "12 Stage Avenue", City = "New York City", State ="New York", Zipcode ="11341", Price = 34.99M, StartDate = new DateTime(2018, 9, 23, 20, 0, 0), EndDate = new DateTime(2018, 9, 23, 22, 0, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/15" },
                new Event() {EventTypeId=5,EventCategoryId=7, Title = "Surgery in Age of AI", OrganizerId = 2, Address= "349 Hospital Blvd", City = "Redmond", State ="Washington", Zipcode ="98118", Price = 0.0M, StartDate = new DateTime(2018, 7, 5, 0, 0, 0), EndDate = new DateTime(2018, 7, 9, 0, 0, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/9" },
                new Event() {EventTypeId=10,EventCategoryId=10, Title = "Dallas Job Fair", OrganizerId = 3, Address= "90 Broom Avenue", City = "Dallas", State ="Texas", Zipcode ="78110", Price = 0.00M, StartDate = new DateTime(2018, 2, 1, 12, 0, 0), EndDate = new DateTime(2018, 2, 2, 14, 0, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/14" },
                new Event() {EventTypeId=7,EventCategoryId=2, Title = "Volt Resturant Opening", OrganizerId = 9, Address= "45 Landford Close", City = "Frederick", State ="Maryland", Zipcode ="21764", Price = 10.00M, StartDate = new DateTime(2018, 1, 10, 11, 0, 0), EndDate = new DateTime(2018, 1, 10, 12, 0, 0), ImageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/16" },


            };

        }

    }
}
