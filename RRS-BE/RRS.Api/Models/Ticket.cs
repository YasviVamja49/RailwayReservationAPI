using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRS.Api.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Ticket Number")]
        public int Ticketno { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Enter Ticket Class: ")]
        [DisplayName("Class")]
        public string Ticketclass { get; set; }


        [Required(ErrorMessage = "Enter berth number:")]
        [DisplayName("Berth Number")]
        public int Berthno { get; set; }

        [Required(ErrorMessage = "Enter coach number:")]
        [DisplayName("Coach Number")]
        public int Coachno { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Enter Arrival Date: ")]
        [DisplayName("Arrival Date")]
        public string Arrivaldate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Enter Booking Date: ")]
        [DisplayName("Booking Date")]
        public string Bookingdate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "nvarchar")]
        public string Bookingstatus { get; set; }

        [Required]
        [DisplayName("Passenger ID")]
        [ForeignKey("Passenger")]
        public int PassengerId { get; set; }
        [Required]
        [ForeignKey("Train")]
        [DisplayName("Train Number")]
        public int TrainNo { get; set; }

        public virtual Train Train { get; set; }
        public virtual Passenger Passenger { get; set; }
    }
}
