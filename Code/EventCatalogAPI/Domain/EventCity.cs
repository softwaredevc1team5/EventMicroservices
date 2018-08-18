using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Domain
{
    public class EventCity
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public string CityDescription { get; set; }
        public string CityImageUrl { get; set; }
    }
}
