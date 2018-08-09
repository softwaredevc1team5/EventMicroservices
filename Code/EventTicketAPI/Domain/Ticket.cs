using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTicketAPI.Domain
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string TicketDescription { get; set; }
        public int AvailableQty { get; set; }
        public decimal TicketPrice { get; set; }
        public int TotalCapacity { get; set; }
        public int MinTktsPerOrder { get; set; }
        public int MaxTktsPerOrder { get; set; }
        public DateTime SalesStartDate { get; set; }
        public DateTime SalesEndDate { get; set; }
        public int TicketTypeId { get; set; }
        public virtual TicketType TicketType { get; set; }
    }
}
