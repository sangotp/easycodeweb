using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EasyCodeAcademy.Web.Pages.Courses.Learn.Exercise
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly EasyCodeContext _context;

        public IndexModel(EasyCodeContext context)
        {
            _context = context;
        }

        public Course Course { get; set; } = default!;

        public CourseLesson CourseLesson { get; set; } = default!;

        public CourseExerise CourseExercise { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? lessonId, int? exerciseId)
        {
            if (id == null || lessonId == null || _context.courses == null)
            {
                return NotFound();
            }

            if(exerciseId != null)
            {
                var courseExercise = await _context.courseExerises.FirstOrDefaultAsync(m => m.ExerciseId == exerciseId);

                if(courseExercise != null)
                {
                    CourseExercise = courseExercise;
                }
            }

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var course = await _context.courses.Include(m => m.CourseChapters)
                                               .ThenInclude(m => m.CourseLessons)
                                               .ThenInclude(m => m.CourseExerises)
                                               .FirstOrDefaultAsync(m => m.CourseId == id);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var courselesson = await _context.courseLessons.Include(m => m.CourseExerises).FirstOrDefaultAsync(m => m.LessonId == lessonId);

            if (course == null || courselesson == null)
            {
                return NotFound();
            }
            else
            {
                Course = course;
                CourseLesson = courselesson;
            }

            return Page();
        }
    }
}
