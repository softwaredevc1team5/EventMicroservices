using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishListAPI.Domain;

namespace WishListAPI.Data
{
    public class WishListSeed
    {
        public static async Task SeedAsync(WishListContext context)
        {
            context.Database.Migrate();
          
            if (!context.WishCartItem.Any())
            {
                context.WishCartItem.AddRange
                    (GetPreconfiguredWishListItems());

                await context.SaveChangesAsync();
            }

           

        }



        static IEnumerable<WishCartItem> GetPreconfiguredWishListItems()
        {
            return new List<WishCartItem>()
            {

                new WishCartItem() { BuyerId = 1,EventId=1,EventTitle = "A Night with Katy Perry",TicketPrice= new Decimal(100.00),NumOfTickets=2,TicketType="EarlyBird"},
                new WishCartItem() { BuyerId = 1,EventId=1,EventTitle = "A Night with Katy Perry",TicketPrice= new Decimal(0.0),NumOfTickets=3,TicketType="Free"},
                new WishCartItem() { BuyerId = 1,EventId=6,EventTitle = "Feed the Children Gala",TicketPrice= new Decimal(150.00),NumOfTickets=1,TicketType="EarlyBird"},


                new WishCartItem() { BuyerId = 2,EventId=1,EventTitle = "A Night with Katy Perry",TicketPrice= new Decimal(100.00),NumOfTickets=1,TicketType="EarlyBird"},
                new WishCartItem() { BuyerId = 2,EventId=1,EventTitle = "A Night with Katy Perry",TicketPrice= new Decimal(0.0),NumOfTickets=2,TicketType="Free"},
                new WishCartItem() { BuyerId = 2,EventId=6,EventTitle = "Feed the Children Gala",TicketPrice= new Decimal(150.00),NumOfTickets=2,TicketType="EarlyBird"},

                new WishCartItem() { BuyerId = 3,EventId=7,EventTitle = "Learn Python for Free",TicketPrice= new Decimal(0.00),NumOfTickets=5,TicketType="Free"},

                new WishCartItem() { BuyerId = 4,EventId=7,EventTitle = "Learn Python for Free",TicketPrice= new Decimal(0.00),NumOfTickets=2,TicketType="Free"},

                new WishCartItem() { BuyerId = 5,EventId=7,EventTitle = "Learn Python for Free",TicketPrice= new Decimal(0.00),NumOfTickets=10,TicketType="Free"},

                new WishCartItem() { BuyerId = 6,EventId=10,EventTitle = "Professional Singles Meetup",TicketPrice= new Decimal(150.00),NumOfTickets=10,TicketType="EarlyBird"},


            };

        }



       

    }
}
