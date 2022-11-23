using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EasyCodeAcademy.Web.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;

namespace EasyCodeAcademy.Web.Pages_Manage_Courses_Chapters
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;

        public IndexModel(EasyCodeAcademy.Web.Models.EasyCodeContext context)
        {
            _context = context;
        }

        public IList<CourseChapter> CourseChapterList { get; set; } = default!;

        [BindProperty]
        public CourseChapter CourseChapter { get; set; } = default!;

        public Course CourseOfChapter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var coursechapter = await _context.courseChapters.FirstOrDefaultAsync(m => m.CourseId == id);
            //if(coursechapter is not null)
            //{
            //    // Watch Again
            //    var e = _context.Entry(coursechapter);
            //    var courselesson = await _context.courseLessons.FirstOrDefaultAsync(m => m.Chapter.CourseId == id);
            //    if(courselesson is not null)
            //    {
            //        await e.Collection(l => l.CourseLessons).LoadAsync();
            //    }
            //}

            if (id == null || coursechapter == null)
            {
                ViewData["Status"] = "No Chapter Found For This Course";
                return Page();
            }
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            CourseChapterList = await _context.courseChapters.Where(m => m.CourseId == id).Include(c => c.Course)
                                                             .Include(l => l.CourseLessons)
                                                             .ThenInclude(e => e.CourseExerises).ToListAsync();
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            var courseofchapter = await _context.courses.FirstOrDefaultAsync(m => m.CourseId == id);
            
            if(courseofchapter != null)
            {
                CourseOfChapter = courseofchapter;
            }
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            CourseChapter.created_date = DateTime.Now;
            CourseChapter.updated_date = DateTime.Now;

            if (!ModelState.IsValid || _context.courseChapters == null || CourseChapter == null)
            {
                //return Page();
                return BadRequest();
            }

            _context.courseChapters.Add(CourseChapter);
            await _context.SaveChangesAsync();
            return new JsonResult(CourseChapter);
        }
    }
}
