using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCodeAcademy.Web.Models
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar")]
        [StringLength(400)]
        public string? HomeAddress { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        // User - Payment (1 - Many)
        public ICollection<ECAPayment>? ECAPayments { get; set; }

        // User - Comment (1 - Many)
        public ICollection<Comment>? Comments { get; set; }
    }
}
