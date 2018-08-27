using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using OrderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Data
{
    public class SeedData
    {
        public static async Task SeedAsync(OrderDbContext context)
        {
            Console.WriteLine("Begining Seeding.");
            Console.Out.Flush();
            try
            {
                context.Database.Migrate();
                if (!context.Orders.Any())
                {
                    context.Orders.AddRange();
                    await context.SaveChangesAsync();
                }
                if (!context.OrderTicket.Any())
                {
                    context.OrderTicket.AddRange();
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Seed failed. " + ex);
            }



        }
    }
}
        