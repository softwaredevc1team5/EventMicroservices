using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OrderApi.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderTicket> OrderTicket { get; set; }

        protected override void OnModelCreating
            (ModelBuilder builder)
        {
            builder.Entity<Order>(ConfigureOrder);
            builder.Entity<OrderTicket>(ConfigureOrderItem);
            
        }
        private void ConfigureOrder(EntityTypeBuilder<Order> builder)
        {
            try
            {
                builder.ToTable("Order");
                builder.Property(c => c.OrderId)
                    .ForSqlServerUseSequenceHiLo("order_hilo")
                    .IsRequired();
                builder.Property(c => c.OrderDate)
                    .IsRequired()
                    .HasMaxLength(40);
                builder.Property(c => c.OrderTotal)
                    .IsRequired()
                    .HasMaxLength(500);
                builder.Property(c => c.FirstName)
                    .IsRequired();
                builder.Property(c => c.LastName)
                    .IsRequired();
                builder.Property(c => c.Address)
                    .IsRequired();
                builder.Property(c => c.UserName)
                    .IsRequired();
                builder.Property(c => c.BuyerId)
                    .IsRequired();
                builder.Property(c => c.StripeToken)
                    .IsRequired();
                builder.Property(c => c.OrderStatus)
                    .IsRequired();
                builder.Property(c => c.EventId)
                    .IsRequired();
                builder.Property(c => c.EventTitle)
                    .IsRequired();
                builder.Property(c => c.EventStartDate)
                    .IsRequired();
                builder.Property(c => c.EventEndDate)
                    .IsRequired();
                builder.Property(c => c.PictureUrl)
                    .IsRequired();
                builder.Property(c => c.PaymentAuthCode)
                    .IsRequired();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something bad happened " + ex);
            }
        }
        private void ConfigureOrderItem(EntityTypeBuilder<OrderTicket> builder)
        {
            try
            {
                //throw new NotImplementedException();
                builder.ToTable("OrderTicket");
                builder.Property(c => c.TicketOrderId)
                    .ForSqlServerUseSequenceHiLo("ticketorder_hilo")
                    .IsRequired();
                builder.Property(c => c.TicketTypeId)
                    .IsRequired()
                    .HasMaxLength(50);
                builder.Property(c => c.TypeName)
                    .IsRequired();
                builder.Property(c => c.Quantity)
                   .IsRequired();
                builder.Property(c => c.Price)
                    .IsRequired();
                builder.Property(c => c.ImageUrl)
                    .IsRequired();
                builder.Property(c => c.EventId)
                    .IsRequired();
                builder.HasOne(c => c.Order)
                    .WithMany()
                    .HasForeignKey(c => c.OrderId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something bad happened creating the order item: " + ex);
            }
        }
        
    }
}




