using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

                new Event() {EventTypeID=1,EventCategoryId=3, Title = "A Night with Katy Perry",  Price = 49.5M, StartDate = new DateTime(2018, 8, 10, 7, 10, 0), EndDate = new DateTime(2018, 18, 10, 9, 15, 0), imageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1" },
                new Event() {EventTypeID=6,EventCategoryId=5, Title = "Feed the Children Gala",  Price = 500.0M, StartDate = new DateTime(2018, 4, 16, 20, 30, 0), EndDate = new DateTime(2018, 4, 16, 23, 0, 0), imageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2" },
                new Event() {EventTypeID=7,EventCategoryId=1, Title = "Learn Python for Free",  Price = 0.0M, StartDate = new DateTime(2018, 3, 6, 12, 0, 0), EndDate = new DateTime(2018, 3, 8, 12, 0, 0), imageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3" },
                new Event() {EventTypeID=3,EventCategoryId=4, Title = "Burger Fest",  Price = 0.0M, StartDate = new DateTime(2018, 9, 27, 9, 0, 0), EndDate = new DateTime(2018, 9, 27, 17, 30, 0), imageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4" },
                new Event() {EventTypeID=2,EventCategoryId=3, Title = "Death of a Salesman",  Price = 34.99M, StartDate = new DateTime(2018, 9, 23, 20, 0, 0), EndDate = new DateTime(2018, 9, 23, 22, 0, 0), imageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5" },
                new Event() {EventTypeID=5,EventCategoryId=7, Title = "Tropical Epedimelogy 2018",  Price = 300.00M, StartDate = new DateTime(2018, 7, 5, 0, 0, 0), EndDate = new DateTime(2018, 7, 9, 0, 0, 0), imageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6" },
                new Event() {EventTypeID=10,EventCategoryId=10, Title = "Professional Singles Meetup",  Price = 10.00M, StartDate = new DateTime(2018, 2, 1, 12, 0, 0), EndDate = new DateTime(2018, 2, 2, 14, 0, 0), imageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7" },
                new Event() {EventTypeID=7,EventCategoryId=2, Title = "Blockchains's Future in  WallStreet",  Price = 0.00M, StartDate = new DateTime(2018, 1, 10, 11, 0, 0), EndDate = new DateTime(2018, 1, 10, 12, 0, 0), imageUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },
                

            };

        }
    }
}
