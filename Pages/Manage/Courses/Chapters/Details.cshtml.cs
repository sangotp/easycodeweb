using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EasyCodeAcademy.Web.Models;
using NuGet.Protocol;

namespace EasyCodeAcademy.Web.Pages_Manage_Courses_Chapters
{
    public class DetailsModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;

        public DetailsModel(EasyCodeAcademy.Web.Models.EasyCodeContext context)
        {
            _context = context;
        }

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

        // OnGetAsync For Details Course Content
        public async Task<IActionResult> OnGetDetailsCourseContent(int? id)
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

        // OnGetAsync For Details Lesson Content
        public async Task<IActionResult> OnGetDetailsLessonContent(int? id)
        {
            if (id == null || _context.courseLessons == null)
            {
                return NotFound();
            }

            var courselesson = await _context.courseLessons.FirstOrDefaultAsync(m => m.LessonId == id);

            if (courselesson == null)
            {
                return NotFound();
            } else
            {
                CourseLesson = courselesson;
            }

            return new JsonResult(courselesson);
        }

        // OnGetAsync For Details Exercise Content
        public async Task<IActionResult> OnGetDetailsExerciseContent(int? id)
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

            return new JsonResult(CourseExercise);
        }
    }
}
