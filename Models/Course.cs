using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCodeAcademy.Web.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [StringLength(255, MinimumLength = 1, ErrorMessage = "{0} required {2} - {1} characters")]
        [Required]
        [DisplayName("Course Name")]
        public string? CourseName { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Course Desc")]
        public string? CourseDescription { get; set; }

        //[DisplayFormat(DataFormatString = "{0:C0}")]
        [Column(TypeName = "Money")]
        [DisplayName("Course Price")]
        [Required]
        public decimal CoursePrice { get; set; }

        [Required]
        [DisplayName("Type")]
        public int CourseType { get; set; }

        [Required]
        [DisplayName("Status")]
        public int CourseStatus { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        [DisplayName("Image")]
        public string? CourseImage { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created Date")]
        public DateTime created_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Updated Date")]
        public DateTime updated_date { get; set; }

        [DisplayName("Topic")]
        public int TopicId { get; set; }

        public Topic? Topic { get; set; }

        // Course - CourseDetails (1 - 1)
        public CourseDetails? CourseDetails { get; set; }

        // Course - CourseChapter (1 - Many)
        public ICollection<CourseChapter>? CourseChapters { get; set; }
    }
}
