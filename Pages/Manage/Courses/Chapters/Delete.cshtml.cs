using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EasyCodeAcademy.Web.Models;

namespace EasyCodeAcademy.Web.Pages_Manage_Courses_Chapters
{
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
        public CourseChapter CourseChapter { get; set; } = default!;

        public CourseLesson CourseLesson { get; set; } = default!;

        public CourseExerise CourseExercise { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.courseChapters == null)
            {
                return NotFound();
            }

            var coursechapter = await _context.courseChapters.FirstOrDefaultAsync(m => m.ChapterId == id);

            if (coursechapter == null)
            {
                return NotFound();
            }
            else 
            {
                CourseChapter = coursechapter;
            }
            return Page();
        }

        // OnGetAsync For Delete Course Content
        public async Task<IActionResult> OnGetDeleteCourseContent(int? id)
        {
            if (id == null || _context.courseChapters == null)
            {
                return NotFound();
            }

            var coursechapter = await _context.courseChapters.FirstOrDefaultAsync(m => m.ChapterId == id);

            if (coursechapter == null)
            {
                return NotFound();
            }
            else
            {
                CourseChapter = coursechapter;
            }
            return new JsonResult(coursechapter);
        }

        // OnGetAsync For Delete Course Content
        public async Task<IActionResult> OnGetDeleteCourseLesson(int? id)
        {
            if (id == null || _context.courseLessons == null)
            {
                return NotFound();
            }

            var courselesson = await _context.courseLessons.FirstOrDefaultAsync(m => m.LessonId == id);

            if (courselesson == null)
            {
                return NotFound();
            }
            else
            {
                CourseLesson = courselesson;
            }
            return new JsonResult(courselesson);
        }

        // OnGetAsync For Delete Course Content
        public async Task<IActionResult> OnGetDeleteCourseExercise(int? id)
        {
            if (id == null || _context.courseExerises == null)
            {
                return NotFound();
            }

            var courseExercise = await _context.courseExerises.FirstOrDefaultAsync(m => m.ExerciseId == id);

            if (courseExercise == null)
            {
                return NotFound();
            }
            else
            {
                CourseExercise = courseExercise;
            }
            return new JsonResult(courseExercise);
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.courseChapters == null)
            {
                return NotFound();
            }
            var coursechapter = await _context.courseChapters.FindAsync(id);

            if (coursechapter != null)
            {
                CourseChapter = coursechapter;
                _context.courseChapters.Remove(CourseChapter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        // OnPostAsync For Delete Course Content
        public async Task<IActionResult> OnPostDeleteCourseContent(int? id)
        {
            if (id == null || _context.courseChapters == null)
            {
                return NotFound();
            }
            var coursechapter = await _context.courseChapters.FindAsync(id);

            if (coursechapter != null)
            {
                CourseChapter = coursechapter;
                _context.courseChapters.Remove(CourseChapter);
                await _context.SaveChangesAsync();
            }

            return new JsonResult(coursechapter);
        }

        // OnPostAsync For Delete Course Lesson
        public async Task<IActionResult> OnPostDeleteCourseLesson(int? id)
        {
            if (id == null || _context.courseLessons == null)
            {
                return NotFound();
            }
            var courselesson = await _context.courseLessons.FindAsync(id);

            if (courselesson != null)
            {
                CourseLesson = courselesson;

                if (CourseLesson.Video != null)
                {
                    var filepath = Path.Combine(_env.WebRootPath, "Assets/uploads/video", CourseLesson.Video);
                    System.IO.File.Delete(filepath);
                }

                _context.courseLessons.Remove(CourseLesson);
                await _context.SaveChangesAsync();
            }

            return new JsonResult(courselesson);
        }

        // OnPostAsync For Delete Course Lesson
        public async Task<IActionResult> OnPostDeleteCourseExercise(int? id)
        {
            if (id == null || _context.courseExerises == null)
            {
                return NotFound();
            }
            var courseExercise = await _context.courseExerises.FindAsync(id);

            if (courseExercise != null)
            {
                CourseExercise = courseExercise;

                _context.courseExerises.Remove(CourseExercise);
                await _context.SaveChangesAsync();
            }

            return new JsonResult(courseExercise);
        }
    }
}
