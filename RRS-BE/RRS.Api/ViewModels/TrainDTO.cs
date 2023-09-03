using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRS.Api.ViewModels
{
    public class TrainDTO
    {
        public int TrainNo { get; set; }

        [Required(ErrorMessage = "Enter Train Name: ")]
        [StringLength(50)]
        public string TrainName { get; set; }

        [Required(ErrorMessage = "Enter Starting Location: ")]
        [StringLength(50)]
        public string Startloc { get; set; }

        [Required(ErrorMessage = "Enter Destination: ")]
        [StringLength(50)]
        public string Endloc { get; set; }

        [Required(ErrorMessage = "Enter Arrival Time: ")]
        [StringLength(50)]
        public string Arrivaltime { get; set; }

        [Required(ErrorMessage = "Enter Departure Time: ")]
        [StringLength(50)]
        public string Departuretime { get; set; }

        [Required(ErrorMessage = "Enter Departure Date: ")]
        [StringLength(50)]
        public string Arrivaldate { get; set; }

        [Required(ErrorMessage = "Enter AC-1 tier seats: ")]
        public int Ac1tier { get; set; }

        [Required(ErrorMessage = "Enter AC-2 tier seats: ")]
        public int Ac2tier { get; set; }

        [Required(ErrorMessage = "Enter AC-3 tier seats: ")]
        public int Ac3tier { get; set; }

        [Required(ErrorMessage = "Enter Sleeper Class seats: ")]
        public int Sleeper { get; set; }

        [Required(ErrorMessage = "Enter Tatkal Class seats: ")]
        public int Tatkal { get; set; }

        [Required(ErrorMessage = "Enter Ladies Class seats: ")]
        public int Ladies { get; set; }

        [Required(ErrorMessage = "Enter Base Fare: ")]
        public int BaseFare { get; set; }
    }
}
