using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EasyCodeAcademy.Web.Models;

namespace EasyCodeAcademy.Web.Pages_Manage_Courses
{
    public class DetailsModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;

        public DetailsModel(EasyCodeAcademy.Web.Models.EasyCodeContext context)
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

            var course = await _context.courses.Include(c => c.Topic).FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }
            else 
            {
                Course = course;
                var e = _context.Entry(Course);
                await e.Reference(d => d.CourseDetails).LoadAsync();
            }
            return Page();
        }
    }
}
