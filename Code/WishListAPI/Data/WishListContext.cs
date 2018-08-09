using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishListAPI.Domain;

namespace WishListAPI.Data
{
    public class WishListContext : DbContext
    {
        public WishListContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<WishCartItem> WishCartItems { get; set; }
        //  public DbSet<WishCart> WishCart { get; set; }
        protected override void OnModelCreating
            (ModelBuilder builder)
        {
            builder.Entity<WishCartItem>(ConfigureWishCartItems);
            // builder.Entity<WishCart>(ConfigureWishCart);

        }
        private void ConfigureWishCartItems(EntityTypeBuilder<WishCartItem> builder)
        {

            builder.ToTable("WishCartItems");
            builder.Property(c => c.Id)
                .ForSqlServerUseSequenceHiLo("WishCartItem_hilo")
                .IsRequired();
            builder.Property(c => c.EventId)
                .IsRequired();
            builder.Property(c => c.EventTitle)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.TicketPrice)
                .IsRequired();
            builder.Property(c => c.NumOfTickets)
                .IsRequired();
            builder.Property(c => c.TicketType)
                .IsRequired()
                .HasMaxLength(50);
        }

    }
}

