
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EventCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Data
{
    public class EventCatalogContext:DbContext
    {
        public EventCatalogContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCity> EventCities { get; set; }

        protected override void OnModelCreating
            (ModelBuilder builder)
        {
            builder.Entity<EventType>(ConfigureEventType);
            builder.Entity<EventCategory>(ConfigureEventCategory);
            builder.Entity<Event>(ConfigureEvent);
            builder.Entity<EventCity>(ConfigureEventCity);
        }
        private void ConfigureEventCity(EntityTypeBuilder<EventCity> builder)
        {
            builder.ToTable("EventCity");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("event_city_hilo")
                .IsRequired();
            builder.Property(c => c.CityName)
                .IsRequired()
                .HasMaxLength(40);
            builder.Property(c => c.CityDescription)
                .IsRequired()
                .HasMaxLength(500);
            builder.Property(c => c.CityImageUrl)
                .IsRequired(false);
        }
        private void ConfigureEvent(EntityTypeBuilder<Event> builder)
        {
            //throw new NotImplementedException();
            builder.ToTable("Event");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("event_hilo")
                .IsRequired();
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.OrganizerId)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.OrganizerName)
               .IsRequired()
               .HasMaxLength(100);
            builder.Property(c => c.ImageUrl)
                .IsRequired(false);
            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(75);
            builder.Property(c => c.City)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.State)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.Zipcode)
                .IsRequired()
                .HasMaxLength(11);
            builder.Property(c => c.Price)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(c => c.StartDate)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(c => c.EndDate)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasOne(c => c.EventType)
                .WithMany()
                .HasForeignKey(c => c.EventTypeId);

            builder.HasOne(c => c.EventCategory)
                .WithMany()
                .HasForeignKey(c => c.EventCategoryId);

        }
        private void ConfigureEventType(EntityTypeBuilder<EventType> builder)
        {
            //throw new NotImplementedException();
            builder.ToTable("EventType");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("event_type_hilo")
                .IsRequired();
            builder.Property(c => c.Type)
                .IsRequired()
                .HasMaxLength(100);
        }
        private void ConfigureEventCategory(EntityTypeBuilder<EventCategory> builder)
        {
            //throw new NotImplementedException();

            builder.ToTable("EventCategory");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("event_category_hilo")
                .IsRequired();
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.ImageUrl)
                .IsRequired(false);

        }
}
}
