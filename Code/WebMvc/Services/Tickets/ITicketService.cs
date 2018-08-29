using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models.Ticket;

namespace WebMvc.Services.Tickets
{
   public  interface ITicketService
    {

        Task<Ticket> GetTicketById(int id);
        Task<IEnumerable<SelectListItem>> GetTicketsByEventId(int id);
        Task<IEnumerable<SelectListItem>> GetTicketsByEventTitle(string  title);
        Task<IEnumerable<SelectListItem>> GetAllTicketTypes();

    }
}
