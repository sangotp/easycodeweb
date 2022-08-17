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
                course = await _context.courses.Include(m => m.CourseDetails).FirstOrDefaultAsync(m => m.CourseId == id);
                Course = course;
            }
            return Page();
        }
    }
}
