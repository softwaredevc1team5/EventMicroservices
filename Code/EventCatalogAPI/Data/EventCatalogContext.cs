using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        
        protected override void OnModelCreating
            (ModelBuilder builder)
        {
            builder.Entity<EventType>(ConfigureEventType);
            builder.Entity<EventCategory>(ConfigureEventCategory);
            builder.Entity<Event>(ConfigureEvent);
        }
        private void ConfigureEvent(EntityTypeBuilder<Event> builder)
        {
            //throw new NotImplementedException();
            builder.ToTable("Catalog");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("catalog_hilo")
                .IsRequired();
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.ImageUrl)
                .IsRequired(false);
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

        }
}
}
