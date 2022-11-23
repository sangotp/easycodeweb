using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCodeAcademy.Web.Models
{
    public class ECAPayment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [DisplayName("PayPal Payment ID")]
        public string? paymentId { get; set; }

        [DisplayName("Payment Method")]
        public string? Method { get; set; }

        [DisplayName("Status")]
        public int Status { get; set; }

        [DisplayName("Course")]
        public int courseId { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created Date")]
        public DateTime created_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Updated Date")]
        public DateTime updated_date { get; set; }

        [DisplayName("User")]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser? User { get; set; }
    }
}
