using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EasyCodeAcademy.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyCodeAcademy.Web.Pages.Courses.Learn
{
    public class IndexModel : PageModel
    {
        private readonly EasyCodeContext _context;

        public IndexModel(EasyCodeContext context)
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

            if (course == null)
            {
                return NotFound();
            }
            else
            {
                Course = course;
            }
            return Page();
        }
    }
}
