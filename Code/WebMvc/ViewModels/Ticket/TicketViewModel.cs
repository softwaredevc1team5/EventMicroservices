using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.ViewModels.Ticket
{
    public class TicketViewModel
    {

        [Required]
        [Display(Name = "Available")]
        public int AvailableQty { get; set; }

        [Required]      
        [Display(Name = "Price")]
        public Decimal TicketPrice { get; set; }
       
        [Display(Name = "Capacity")]
        public string Capacity { get; set; }
    }
}
