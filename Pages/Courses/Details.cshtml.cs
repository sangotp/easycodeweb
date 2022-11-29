using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace EasyCodeAcademy.Web.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly EasyCodeContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public DetailsModel(EasyCodeContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Course Course { get; set; } = default!;

        public CourseLesson CourseLesson { get; set; } = default!;

        public IList<ECAPayment> ECAPayments { get; set; } = default!;

        [BindProperty]
        public Comment Comment { get; set; } = default!;

        public IList<Comment> Comments { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var course = await _context.courses.FirstOrDefaultAsync(m => m.CourseId == id);

            var courseDetails = await _context.courseDetails.FirstOrDefaultAsync(d => d.CourseDetailsId == id);

            var comments = await _context.Comments.Where(c => c.CourseId == id).Include(u => u.User).ToListAsync();

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

            if (comments != null)
            {
                Comments = comments;
            }

            if (_userManager.GetUserId(User) != null)
            {
                ECAPayments = await _context.ECAPayments.Where(u => u.UserId == _userManager.GetUserId(User).ToString())
                                                .ToListAsync();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var userComment = await _context.Comments.Where(c => c.CourseId == id).Where(u => u.UserId == _userManager.GetUserId(User).ToString()).FirstOrDefaultAsync();

            if(userComment != null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }

            Comment.created_date = DateTime.Now;
            Comment.updated_date = DateTime.Now;
            Comment.UserId = _userManager.GetUserId(User).ToString();
            //if(Comment.CommentMessage == null)
            //{
            //    return BadRequest();
            //}
            if(id != null)
            {
                Comment.CourseId = (int)id;
            }

            if (!ModelState.IsValid || _context.Comments == null || Comment == null)
            {
                return BadRequest();
            }

            _context.Comments.Add(Comment);

            await _context.SaveChangesAsync();

            return new JsonResult(Comment);
        }
    }
}
