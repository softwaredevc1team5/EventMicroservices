using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models.Orders;

namespace WebMvc.ViewModels.Order
{
    public class DetailViewModel
    {   
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal OrderTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string BuyerId { get; set; }
        public string StripeToken { get; set; }
        
        //EventProperties
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public string PictureUrl { get; set; }
        public string PaymentAuthCode { get; set; }
        public List<OrderTicket> OrderTicket { get; } = new List<OrderTicket>();
    }
}
