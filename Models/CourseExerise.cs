using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCodeAcademy.Web.Models
{
    public class CourseExerise
    {
        [Key]
        public int ExerciseId { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} required {2} - {1} characters")]
        [Required]
        [DisplayName("Exercise Title")]
        public string? Title { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Exercise Description")]
        public string? Description { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        [DisplayName("Exercise Content")]
        public string? Content { get; set; }

        [Required]
        [DisplayName("Exercise Status")]
        public int Status { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created Date")]
        public DateTime created_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Updated Date")]
        public DateTime updated_date { get; set; }

        [DisplayName("Lesson")]
        [ForeignKey("LessonId")]
        public int LessonId { get; set; }

        public CourseLesson? Lesson { get; set; }
    }
}
