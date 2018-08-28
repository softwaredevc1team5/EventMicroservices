using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventMicroservices.Services.CartApi.Model
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(string cartId);
         IEnumerable<string>  GetUsers();
        Task<Cart> UpdateCartAsync(Cart cartbasket);
        Task<bool> DeleteCartAsync(string id);
    }
}
