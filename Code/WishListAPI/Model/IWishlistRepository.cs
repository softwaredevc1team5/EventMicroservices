using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishListAPI.Model
{
    public interface IWishlistRepository
    {
        Task<Wishlist> GetWishlistAsync(string wishlistId);
        IEnumerable<string> GetUsers();
        Task<Wishlist> UpdateWishlistAsync(Wishlist basket);
        Task<bool> DeleteWishlistAsync(string id);
        void SetEventIdFromMessaging(int id, String buyerId);
        Task<string> GetEventIdFromMessaging(string eventKey);

    }
}
