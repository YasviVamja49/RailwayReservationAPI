using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RRS.Api.ViewModels
{
    public class TicketDTOinput
    {
        

        [Required(ErrorMessage = "Enter Ticket Class")]
        [StringLength(50)]
        [Display(Name = "Class")]
        public string Ticketclass { get; set; }


        [Required]
        [Display(Name = "Passenger Name")]
        public string PassengerName { get; set; }


        [Required]
        [Display(Name = "Train Number")]
        public int TrainNo { get; set; }
    }
}
