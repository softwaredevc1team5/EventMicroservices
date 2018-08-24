using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.ViewModels
{
    public class WishlistComponentViewModel
    {
        public int ItemsInWishlist { get; set; }
        public string Disabled => (ItemsInWishlist == 0) ? "is-disabled" : "";

    }
}
