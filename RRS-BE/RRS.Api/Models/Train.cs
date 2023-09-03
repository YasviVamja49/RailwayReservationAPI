using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRS.Api.Models
{
    public class Train
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Train Number")]
        public int TrainNo { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Enter Train Name: ")]
        [DisplayName("Train Name")]
        public string TrainName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Enter Starting Location: ")]
        [DisplayName("Starting Location")]
        public string Startloc { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Enter Destination: ")]
        [DisplayName("Destination")]
        public string Endloc { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Enter Arrival Time: ")]
        [DisplayName("Arrival Time")]
        public string Arrivaltime { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Enter Departure Time: ")]
        [DisplayName("Departure Time")]
        public string Departuretime { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Enter Departure Date: ")]
        [DisplayName("Departure Date")]
        public string Arrivaldate { get; set; }

        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Enter AC-1 tier seats: ")]
        [DisplayName("AC-1 tier seats")]
        public int Ac1tier { get; set; }

        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Enter AC-2 tier seats: ")]
        [DisplayName("AC-2 tier seats")]
        public int Ac2tier { get; set; }

        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Enter AC-3 tier seats: ")]
        [DisplayName("AC-3 tier seats")]
        public int Ac3tier { get; set; }

        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Enter Sleeper Class seats: ")]
        [DisplayName("Sleeper Class Seats")]
        public int Sleeper { get; set; }

        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Enter Tatkal Class seats: ")]
        [DisplayName("Tatkal Class Seats")]
        public int Tatkal { get; set; }

        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Enter Ladies Class seats: ")]
        [DisplayName("Ladies Class Seats")]
        public int Ladies { get; set; }

        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Enter Base Fare: ")]
        [DisplayName("Base Fare")]
        public int BaseFare { get; set; }
    }
}
