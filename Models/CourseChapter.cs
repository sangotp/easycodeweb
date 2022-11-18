using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCodeAcademy.Web.Models
{
    public class CourseChapter
    {
        [Key]
        public int ChapterId { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} required {2} - {1} characters")]
        [Required]
        [DisplayName("Chapter Title")]
        public string? Title { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Chapter Description")]
        public string? Description { get; set; }

        [Required]
        [DisplayName("Status")]
        public int Status { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created Date")]
        public DateTime created_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Updated Date")]
        public DateTime updated_date { get; set; }

        [DisplayName("Course")]
        public int CourseId { get; set; }

        public Course? Course { get; set; }

        // CourseChapter - CourseLesson (1 - Many)
        public ICollection<CourseLesson>? CourseLessons { get; set; }
    }
}
