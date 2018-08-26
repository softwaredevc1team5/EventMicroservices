using OrderApi.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Models
{
    public class OrderTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public int TicketTypeId { get; set; }

        public string TypeName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int EventId { get; set; }

        public string ImageUrl { get; set; }

        protected OrderTicket() { }
        public Order Order { get; set; }

        


        public OrderTicket(int orderId, int ticketTypeId, decimal unitPrice, string typeName, int units, int eventId, string pictureUrl)
        {
            if (units <= 0)
            {
                throw new OrderingDomainException("Invalid number of units");
            }

            OrderId = orderId;

            TicketTypeId = ticketTypeId;
            Price = unitPrice;

            Quantity = units;
            TypeName = typeName;

            EventId = eventId;
            ImageUrl = pictureUrl;
        }
        public void SetPictureUri(string pictureUri)
        {
            if (!String.IsNullOrWhiteSpace(pictureUri))
            {
                ImageUrl = pictureUri;
            }
        }

        public void AddUnits(int units)
        {
            if (units < 0)
            {
                throw new OrderingDomainException("Invalid units");
            }

            Quantity += units;
        }
    }
}
