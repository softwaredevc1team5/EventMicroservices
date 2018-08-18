using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebMvc.Services
{
    public interface IIdentityService<T>
    {
        //Get a Token
        T Get(IPrincipal principal);
    }
}
