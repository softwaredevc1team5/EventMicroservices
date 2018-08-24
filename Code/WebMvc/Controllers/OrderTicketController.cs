using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Controllers
{
    public class OrderTicketController:Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
