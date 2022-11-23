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
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace EasyCodeAcademy.Web.Pages_Manage_Courses
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;

        private readonly IWebHostEnvironment _env;

        [DataType(DataType.Upload)]
        [BindProperty]
        [DisplayName("File Upload")]
        public IFormFile? FileUpload { get; set; }

        public EditModel(EasyCodeAcademy.Web.Models.EasyCodeContext context, IWebHostEnvironment env)
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

            var course =  await _context.courses.FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            Course = course;
            var e = _context.Entry(Course);
            if(e.Reference(d => d.CourseDetails) is not null)
            {
                await e.Reference(d => d.CourseDetails).LoadAsync();
            }
            
            ViewData["TopicId"] = new SelectList(_context.topics, "TopicId", "TopicName");
            if(Course.CourseDetails != null)
            {
                if (Course.CourseDetails.CourseRequirement is not null)
                {
                    ViewData["CourseRequirements"] = Course.CourseDetails.CourseRequirement.Split(",");
                }

                if (Course.CourseDetails.CourseGain is not null)
                {
                    ViewData["CourseGains"] = Course.CourseDetails.CourseGain.Split(",");
                }
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["TopicId"] = new SelectList(_context.topics, "TopicId", "TopicName");
                return Page();
            }

            if (FileUpload != null)
            {
                if(Course.CourseImage != null)
                {
                    var filepathprev = Path.Combine(_env.WebRootPath, "Assets/uploads", Course.CourseImage);
                    System.IO.File.Delete(filepathprev);
                }

                var filepath = Path.Combine(_env.WebRootPath, "Assets/uploads", FileUpload.FileName);

                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(filestream);
                }
                
                Course.CourseImage = FileUpload.FileName;
            }

            _context.Attach(Course).State = EntityState.Modified;
            _context.Attach(Course.CourseDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(Course.CourseId))
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

        private bool CourseExists(int id)
        {
          return (_context.courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}
