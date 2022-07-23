using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCodeAcademy.Web.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(255)]
        [Required]
        [Column(TypeName = "nvarchar")]
        public string CategoryName { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime created_date { get; set; }

        [DataType(DataType.Date)]
        public DateTime updated_date { get; set; }
    }
}
