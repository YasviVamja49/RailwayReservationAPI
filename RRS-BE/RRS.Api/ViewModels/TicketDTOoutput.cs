using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RRS.Api.ViewModels
{
    public class TicketDTOoutput:TicketDTOinput
    {
        [Display(Name = "Ticket Number")]
        public int Ticketno { get; set; }

        [Required(ErrorMessage = "Enter berth number")]
        [Display(Name = "Berth Number")]
        public int Berthno { get; set; }

        [Required(ErrorMessage = "Enter coach number")]
        [Display(Name = "Coach Number")]
        public int Coachno { get; set; }

        [Required(ErrorMessage = "Enter Departure Date")]
        [StringLength(50)]
        [Display(Name = "Departure Date")]
        public string Arrivaldate { get; set; }
        [Required(ErrorMessage = "Enter Booking Date")]
        [StringLength(50)]
        [Display(Name = "Booking Date")]
        public string Bookingdate { get; set; }

        [StringLength(20)]
        [Display(Name = "Booking Status")]
        public string Bookingstatus { get; set; }

        

    }
}
