using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EasyCodeAcademy.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.IO;

namespace EasyCodeAcademy.Web.Pages_Manage_Courses_Chapters
{
    public class CreateModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;

        private readonly IWebHostEnvironment _env;

        public CreateModel(EasyCodeAcademy.Web.Models.EasyCodeContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult OnGet()
        {
            ViewData["CourseId"] = new SelectList(_context.courses, "CourseId", "CourseName");
            return Page();
        }

        [BindProperty]
        public CourseChapter CourseChapter { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            CourseChapter.created_date = DateTime.Now;
            CourseChapter.updated_date = DateTime.Now;
            if (!ModelState.IsValid || _context.courseChapters == null || CourseChapter == null)
            {
                return Page();
            }

            _context.courseChapters.Add(CourseChapter);
            await _context.SaveChangesAsync();

            return Page();
        }

        [BindProperty]
        [DataType(DataType.Upload)]
        public IFormFile? FileUpload { get; set; }

        //[RequestFormLimits(MultipartBodyLengthLimit = 409715200)]
        //[RequestSizeLimit(409715200)]
        public async Task<IActionResult> OnPostCreateCourseLesson(CourseLesson CourseLesson)
        {
            if (FileUpload != null)
            {
                var filepath = Path.Combine(_env.WebRootPath, "Assets/uploads/video", FileUpload.FileName);
                using var filestream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                await FileUpload.CopyToAsync(filestream);

                CourseLesson.Video = FileUpload.FileName;
            }
            //else
            //{
            //    CourseLesson.Video = "Empty";
            //}

            CourseLesson.created_date = DateTime.Now;
            CourseLesson.updated_date = DateTime.Now;

            if (_context.courseLessons == null || CourseLesson == null)
            {
                return BadRequest();
            }

            _context.courseLessons.Add(CourseLesson);
            await _context.SaveChangesAsync();

            return new JsonResult(CourseLesson);
        }

        public async Task<IActionResult> OnPostCreateLessonExercise(CourseExerise CourseExercise)
        {
            CourseExercise.created_date = DateTime.Now;
            CourseExercise.updated_date = DateTime.Now;

            if (_context.courseExerises == null || CourseExercise == null)
            {
                return BadRequest();
            }

            _context.courseExerises.Add(CourseExercise);
            await _context.SaveChangesAsync();

            return new JsonResult(CourseExercise);
        }
    }
}
