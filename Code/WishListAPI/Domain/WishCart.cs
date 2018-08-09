using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishListAPI.Domain
{
    public class WishCart
    {
        List<WishCartItem> Tickets;
        public int BuyerId { get; set; }

        public WishCart(int userId)
        {
            BuyerId = userId;
            Tickets = new List<WishCartItem>();
        }

    }
}
