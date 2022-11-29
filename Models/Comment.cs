using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCodeAcademy.Web.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        [DisplayName("Comment Message")]
        public string? CommentMessage { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created Date")]
        public DateTime created_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Updated Date")]
        public DateTime updated_date { get; set; }

        [Required]
        [DisplayName("User")]
        public string UserId { get; set; } = default!;

        [ForeignKey("UserId")]
        public AppUser? User { get; set; }

        [Required]
        [DisplayName("Course")]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
    }
}
