using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCodeAcademy.Web.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "{0} required {2} - {1} characters")]
        [Required(ErrorMessage = "{0} Cannot Empty!")]
        [Column(TypeName = "nvarchar")]
        [DisplayName("Category Name")]
        public string? CategoryName { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created Date")]
        public DateTime created_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Updated Date")]
        public DateTime updated_date { get; set; }

        // Collection Navigation
        public ICollection<Topic>? Topics { get; set; }
    }
}
