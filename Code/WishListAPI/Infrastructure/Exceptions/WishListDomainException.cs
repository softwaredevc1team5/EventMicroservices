using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishListAPI.Infrastructure.Exceptions
{
    public class WishlistDomainException: Exception
    {
        public WishlistDomainException()
        { }
        public WishlistDomainException(string message)
            : base(message)
        { }

        public WishlistDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
