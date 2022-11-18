using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCodeAcademy.Web.Models
{
    public class CourseDetails
    {
        [Key]
        public int CourseDetailsId { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} required {2} - {1} characters")]
        [Required]
        [DisplayName("Language")]
        public string? CourseLanguage { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        [DisplayName("Overview")]
        public string? CourseOverview { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        [DisplayName("Includes")]
        public string? CourseInclude { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        [DisplayName("Requirements")]
        public string? CourseRequirement { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        [DisplayName("Gain")]
        public string? CourseGain { get; set; }

        public Course? Course { get; set; }
    }
}
