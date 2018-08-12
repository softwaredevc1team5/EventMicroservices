using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebMvc.Controllers
{
    public class EventDetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}