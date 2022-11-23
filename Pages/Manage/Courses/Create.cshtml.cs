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
using Microsoft.AspNetCore.Authorization;

namespace EasyCodeAcademy.Web.Pages_Manage_Courses
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;

        private readonly IWebHostEnvironment _env;

        [DataType(DataType.Upload)]
        [Required]
        [BindProperty]
        [DisplayName("File Upload")]
        public IFormFile FileUpload { get; set; }

        public CreateModel(EasyCodeAcademy.Web.Models.EasyCodeContext context, IWebHostEnvironment env)
        {
            _context = context;

            _env = env;
        }

        public IActionResult OnGet()
        {
            ViewData["TopicId"] = new SelectList(_context.topics, "TopicId", "TopicName");
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.courses == null || Course == null)
            {   
                ViewData["TopicId"] = new SelectList(_context.topics, "TopicId", "TopicName");
                if(Course.CourseDetails.CourseRequirement is not null)
                {
                    ViewData["CourseRequirements"] = Course.CourseDetails.CourseRequirement.Split(",");
                }

                if(Course.CourseDetails.CourseGain is not null)
                {
                    ViewData["CourseGains"] = Course.CourseDetails.CourseGain.Split(",");
                }
                return Page();
            }

            if (FileUpload != null)
            {
                var filepath = Path.Combine(_env.WebRootPath, "Assets/uploads", FileUpload.FileName);
                using var filestream = new FileStream(filepath, FileMode.Create);
                await FileUpload.CopyToAsync(filestream);
                Course.CourseImage = FileUpload.FileName;
            }

            _context.courses.Add(Course);
            _context.courseDetails.Add(Course.CourseDetails);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
