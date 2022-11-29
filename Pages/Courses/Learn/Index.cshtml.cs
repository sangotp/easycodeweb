using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EasyCodeAcademy.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EasyCodeAcademy.Web.Pages.Courses.Learn
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly EasyCodeContext _context;
        private readonly UserManager<AppUser> _userManager;

        public IndexModel(EasyCodeContext context,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Course Course { get; set; } = default!;

        public CourseLesson CourseLesson { get; set; } = default!;

        public IList<ECAPayment> ECAPayments { get; set; } = default!;

        public ECAPayment ECAPayment { get; set; } = default!;

        public int? LessonId { get; set; }

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

            var payments = await _context.ECAPayments.Where(u => u.UserId == _userManager.GetUserId(User).ToString()).ToListAsync();
            foreach(var payment in payments)
            {
                if(payment.courseId == id && payment.Status == 1)
                {
                    ECAPayment = payment;
                    break;
                }
            }

            if (course == null || courselesson == null || ECAPayment == null || ECAPayment.Status == 0)
            {
                return NotFound();
            }
            else
            {
                Course = course;
                CourseLesson = courselesson;
                LessonId = lessonId;
            }
            return Page();
        }
    }
}
