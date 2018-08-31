using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models.WishlistModels
{
    public class Wishlist
    {
        public string BuyerId { get; set; }
        public List<WishlistItem> Items { get; set; } = new List<WishlistItem>();
    }
}
