using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCodeAcademy.Web.Models
{
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "{0} required {2} - {1} characters")]
        [DisplayName("Topic Name")]
        [Required(ErrorMessage = "{0} Cannot Empty!")]
        public string? TopicName { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created Date")]
        public DateTime created_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Updated Date")]
        public DateTime updated_date { get; set; }

        [DisplayName("Category")]
        public int CateId { get; set; }

        [ForeignKey("CateId")]
        public Category? Category { get; set; }

        // Collection Navigation
        public ICollection<Course>? Courses { get; set; }
    }
}
