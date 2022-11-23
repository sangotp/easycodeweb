using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EasyCodeAcademy.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EasyCodeAcademy.Web.Pages.Courses.Learn
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

        public async Task<IActionResult> OnGetAsync(int? id, int? lessonId)
        {
            if (id == null || lessonId == null || _context.courses == null)
            {
                return NotFound();
            }

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var course = await _context.courses.Include(m => m.CourseChapters)
                                               .ThenInclude(m => m.CourseLessons)
                                               .ThenInclude(m => m.CourseExerises)
                                               .FirstOrDefaultAsync(m => m.CourseId == id);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var courselesson = await _context.courseLessons.FirstOrDefaultAsync(m => m.LessonId == lessonId);

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
