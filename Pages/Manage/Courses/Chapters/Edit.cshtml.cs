using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyCodeAcademy.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace EasyCodeAcademy.Web.Pages_Manage_Courses_Chapters
{
    public class EditModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;
        private readonly IWebHostEnvironment _env;

        public EditModel(EasyCodeAcademy.Web.Models.EasyCodeContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public CourseChapter CourseChapter { get; set; } = default!;

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
            CourseChapter = coursechapter;
            ViewData["CourseId"] = new SelectList(_context.courses, "CourseId", "CourseImage");
            return Page();
        }

        // OnGetAsync For Edit Course Content
        public async Task<IActionResult> OnGetEditCourseContent(int? id, string Content)
        {
            if (Content == "Chapter")
            {
                var coursechapter = await _context.courseChapters.FirstOrDefaultAsync(m => m.ChapterId == id);

                if (coursechapter != null)
                {
                    return new JsonResult(coursechapter);
                }
            }
            else if (Content == "Lesson")
            {
                var courselesson = await _context.courseLessons.FirstOrDefaultAsync(m => m.LessonId == id);

                if (courselesson != null)
                {
                    return new JsonResult(courselesson);
                }
            }
            else if (Content == "Exercise")
            {
                var courseExercise = await _context.courseExerises.FirstOrDefaultAsync(m => m.ExerciseId == id);

                if (courseExercise != null)
                {
                    return new JsonResult(courseExercise);
                }
            }
            return BadRequest();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CourseChapter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseChapterExists(CourseChapter.ChapterId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        // OnPostAsync For Edit Course Content
        public async Task<IActionResult> OnPostEditCourseContent()
        {
            CourseChapter.updated_date = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Attach(CourseChapter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseChapterExists(CourseChapter.ChapterId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(CourseChapter);
        }

        public async Task<IActionResult> OnPostEditLessonContent(CourseLesson CourseLesson, IFormFile FileUpload)
        {
            if (FileUpload != null)
            {
                if (CourseLesson.Video != null)
                {
                    var filepathprev = Path.Combine(_env.WebRootPath, "Assets/uploads/video", CourseLesson.Video);
                    System.IO.File.Delete(filepathprev);
                }
                var filepath = Path.Combine(_env.WebRootPath, "Assets/uploads/video", FileUpload.FileName);
                using var filestream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                await FileUpload.CopyToAsync(filestream);

                CourseLesson.Video = FileUpload.FileName;
            }

            CourseLesson.updated_date = DateTime.Now;
            //if (!ModelState.IsValid)
            //{
            //    return new JsonResult(CourseLesson);
            //}

            _context.Attach(CourseLesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseLessonExists(CourseLesson.LessonId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(CourseLesson);
        }

        public async Task<IActionResult> OnPostEditExerciseContent(CourseExerise CourseExercise)
        {

            CourseExercise.updated_date = DateTime.Now;

            //if (!ModelState.IsValid)
            //{
            //    return new JsonResult(CourseLesson);
            //}

            _context.Attach(CourseExercise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExerciseExists(CourseExercise.ExerciseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(CourseExercise);
        }

        private bool CourseChapterExists(int id)
        {
            return (_context.courseChapters?.Any(e => e.ChapterId == id)).GetValueOrDefault();
        }

        private bool CourseLessonExists(int id)
        {
            return (_context.courseLessons?.Any(e => e.LessonId == id)).GetValueOrDefault();
        }

        private bool CourseExerciseExists(int id)
        {
            return (_context.courseExerises?.Any(e => e.ExerciseId == id)).GetValueOrDefault();
        }
    }
}
