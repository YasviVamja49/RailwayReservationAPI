using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RRS.Api.Data
{
    public class ApplicationUser:IdentityUser
    {
        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Enter Name")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Enter Date of Birth")]
        [DisplayName("DOB")]
        public string Dob { get; set; }

        [Required(ErrorMessage = "Select your Gender")]
        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        public string Gender { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar")]
        [Required(ErrorMessage = "Enter your City")]
        [DisplayName("City")]
        public string City { get; set; }
    }
}