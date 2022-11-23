using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace EasyCodeAcademy.Web.Pages_Manage_Courses
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;

        private readonly IWebHostEnvironment _env;

        public DeleteModel(EasyCodeAcademy.Web.Models.EasyCodeContext context, IWebHostEnvironment env)
        {
            _context = context;

            _env = env;
        }

        [BindProperty]
      public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var course = await _context.courses.Include(c => c.Topic).FirstOrDefaultAsync(m => m.CourseId == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }
            var course = await _context.courses.FindAsync(id);

            if (course != null)
            {
                Course = course;

                if (Course.CourseImage != null)
                {
                    var filepath = Path.Combine(_env.WebRootPath, "Assets/uploads", Course.CourseImage);
                    System.IO.File.Delete(filepath);
                }

                _context.courses.Remove(Course);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
