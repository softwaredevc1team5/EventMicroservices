using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishListAPI.Domain
{
    public class WishCart
    {
        List<WishCartItem> WishCartList;
        public int BuyerId { get; set; }

    }
}
