using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Data
{
    public class EventCatalogContext: DbContext
    {
        //call constructor to 
        public EventCatalogContext(DbContextOptions options) :
            base(options)
            {

            }



            public DbSet<EventCategory> EventCategories { get; set; }

            public DbSet<EventType> EventTypes { get; set; }

            public DbSet<Event> Events { get; set; }


            protected override void OnModelCreating

                (ModelBuilder builder)

            {

                builder.Entity<EventCategory>(ConfigureEventCategory);

                builder.Entity<EventType>(ConfigureEventType);

                builder.Entity<Event>(ConfigureEvent);

            }

        private void ConfigureEvent(EntityTypeBuilder<Event> obj)
        {
            throw new NotImplementedException();
        }

        private void ConfigureEventType(EntityTypeBuilder<EventType> obj)
        {
            throw new NotImplementedException();
        }

       

        private void ConfigureEventCategory(EntityTypeBuilder<EventCategory> obj)
        {
            throw new NotImplementedException();
        }
    }
    }
