using RRS.Api.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRS.Api.Models
{
    public class Passenger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PassengerId { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Enter Name:")]
        [DisplayName("Name")]
        public string PassengerName { get; set; }

        [Required(ErrorMessage = "Select your Gender:")]
        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        public string Gender { get; set; }

        [ForeignKey("User")]
        public string Id { get; set; }
        public ApplicationUser User { get; set; }
    }
}
