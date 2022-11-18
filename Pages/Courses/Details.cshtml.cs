using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EasyCodeAcademy.Web.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly EasyCodeContext _context;

        public DetailsModel(EasyCodeContext context)
        {
            _context = context;
        }

        public Course Course { get; set; } = default!;

        public CourseLesson CourseLesson { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var course = await _context.courses.FirstOrDefaultAsync(m => m.CourseId == id);

            var courseDetails = await _context.courseDetails.FirstOrDefaultAsync(d => d.CourseDetailsId == id);

            if (course == null || courseDetails == null)
            {
                return NotFound();
            }
            else
            {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                course = await _context.courses.Include(m => m.CourseDetails)
                                               .Include(m => m.CourseChapters)
                                               .ThenInclude(m => m.CourseLessons)
                                               .ThenInclude(m => m.CourseExerises)
                                               .FirstOrDefaultAsync(m => m.CourseId == id);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

                if (course != null)
                {
                    Course = course;
                }
            }
            return Page();
        }
    }
}
