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

namespace EasyCodeAcademy.Web.Pages_Manage_Courses
{
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
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
