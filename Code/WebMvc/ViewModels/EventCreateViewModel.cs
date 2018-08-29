using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class EventCreateViewModel
    {
        public EventCreate Event { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
