using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCodeAcademy.Web.Models
{
    public class CourseLesson
    {
        [Key]
        public int LessonId { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} required {2} - {1} characters")]
        [Required]
        [DisplayName("Lesson Title")]
        public string? Title { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Lesson Description")]
        public string? Description { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Lesson Content")]
        public string? Content { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Video")]
        public string? Video { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Lesson Achievement")]
        public string? Achievement { get; set; }

        [Required]
        [DisplayName("Lesson Status")]
        public int Status { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created Date")]
        public DateTime created_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Updated Date")]
        public DateTime updated_date { get; set; }

        [DisplayName("Chapter")]
        [ForeignKey("ChapterId")]
        public int ChapterId { get; set; }

        public CourseChapter? Chapter { get; set; }

        // CourseLesson - CourseExercise (1 - Many)
        public ICollection<CourseExerise>? CourseExerises { get; set; }
    }
}
