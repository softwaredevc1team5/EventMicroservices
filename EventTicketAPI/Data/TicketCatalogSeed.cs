using EventTicketAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTicketAPI.Data
{
    public class TicketCatalogSeed
    {
        public static async Task SeedAsync(TicketCatalogContext context)
        {
            context.Database.Migrate();
            if (!context.TicketTypes.Any())
            {
                context.TicketTypes.AddRange(GetPreConfiguredTicketTypes());
                await context.SaveChangesAsync();
            }
            if (!context.Tickets.Any())
            {
                context.Tickets.AddRange(GetPreConfiguredTickets());
                context.SaveChanges();
            }
        }
        static IEnumerable<TicketType> GetPreConfiguredTicketTypes()
        {
            return new List<TicketType>()
            {
                new TicketType(){ TypeName = "Free Ticket"},
                new TicketType(){TypeName = "Paid Ticket"},
                new TicketType(){TypeName = "Donation Ticket"}
            };
        }
        static IEnumerable<Ticket> GetPreConfiguredTickets()
        {
            return new List<Ticket>() {
                     new Ticket() {TicketTypeId = 1,EventId = 3,EventTitle = "Feed the Children Gala",AvailableQty = 100,TicketPrice = (decimal)10.0,MinTktsPerOrder = 1,MaxTktsPerOrder = 10,SalesStartDate = new DateTime(2018,10,7,18,0,0),SalesEndDate = new DateTime(2018,10,8,23,59,0)},
                     new Ticket() {TicketTypeId = 2,EventId = 2,EventTitle = "Learn Python for Free",AvailableQty = 100,TicketPrice = (decimal)10.0,MinTktsPerOrder = 1,MaxTktsPerOrder = 10,SalesStartDate = new DateTime(2018,10,7,18,0,0),SalesEndDate = new DateTime(2018,10,8,23,59,0)},
                     new Ticket() {TicketTypeId = 2,EventId = 1,EventTitle = "Disney on Ice",AvailableQty = 50,TicketPrice = (decimal)10.0,MinTktsPerOrder = 1,MaxTktsPerOrder = 10,SalesStartDate = new DateTime(2018,10,7,18,0,0),SalesEndDate = new DateTime(2018,10,8,23,59,0)},
                     new Ticket() {TicketTypeId = 1,EventId = 3,EventTitle = "Feed the Children Gala",AvailableQty = 100,TicketPrice = (decimal)10.0,MinTktsPerOrder = 1,MaxTktsPerOrder = 10,SalesStartDate = new DateTime(2018,10,7,18,0,0),SalesEndDate = new DateTime(2018,10,8,23,59,0)},
                     new Ticket() {TicketTypeId = 3,EventId = 2,EventTitle = "Learn Python for Free",AvailableQty = 100,TicketPrice = (decimal)10.0,MinTktsPerOrder = 1,MaxTktsPerOrder = 10,SalesStartDate = new DateTime(2018,10,7,18,0,0),SalesEndDate = new DateTime(2018,10,8,23,59,0)},
            };
         }
    }
}
