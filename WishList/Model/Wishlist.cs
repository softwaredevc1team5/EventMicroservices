using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishListAPI.Model
{
    public class Wishlist
    {
        public string BuyerId { get; set; }
        public List<WishlistItems> Items { get; set; }

        public Wishlist(string whishlistId)
        {
            BuyerId = whishlistId;
            Items = new List<Model.WishlistItems>();
        }
    }
}
