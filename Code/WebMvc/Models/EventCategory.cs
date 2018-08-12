using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    //mirror EventCategory model/table in EventCatalog API
    public class EventCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
