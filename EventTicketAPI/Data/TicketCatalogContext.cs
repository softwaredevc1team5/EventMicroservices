
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EventTicketAPI.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTicketAPI.Data
{
    public class TicketCatalogContext : DbContext
    {
        public TicketCatalogContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<TicketType>(ConfigureTicketType);
            builder.Entity<Ticket>(ConfigureTicket);
        }

        private void ConfigureTicketType(EntityTypeBuilder<TicketType> builder)
        {
            // throw new NotImplementedException();
            builder.ToTable("TicketType");
            builder.Property(c => c.TypeId)
                .ForSqlServerUseSequenceHiLo("ticket_type_hilo")
                .IsRequired();
           builder.Property(c => c.TypeName)
                .IsRequired()
                .HasMaxLength(100);
        }


        private void ConfigureTicket(EntityTypeBuilder<Ticket> builder)
        {
            //throw new NotImplementedException();
            builder.ToTable("Ticket");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("ticket_hilo")
                .IsRequired();
            builder.Property(c => c.EventId)
                    .IsRequired()
                    .HasMaxLength(8);
            builder.Property(c => c.EventTitle)
                    .IsRequired()
                    .HasMaxLength(25);
            builder.Property(c => c.AvailableQty)
                    .IsRequired()
                    .HasMaxLength(10);
            builder.Property(c => c.TicketPrice)
                    .IsRequired()
                    .HasMaxLength(10);
            builder.Property(c => c.MinTktsPerOrder)
                    .IsRequired()
                    .HasMaxLength(5);
            builder.Property(c => c.MaxTktsPerOrder)
                    .IsRequired()
                    .HasMaxLength(5);
            builder.Property(c => c.SalesStartDate)
                    .IsRequired()
                    .HasMaxLength(20);
            builder.Property(c => c.SalesEndDate)
                    .IsRequired()
                    .HasMaxLength(20);

            builder.HasOne(c => c.TicketType)
                    .WithMany()
                    .HasForeignKey(c => c.TicketTypeId);
        }
    }
}