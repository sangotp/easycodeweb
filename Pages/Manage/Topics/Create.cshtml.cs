using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace EasyCodeAcademy.Web.Pages_Manage_Topics
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;

        public CreateModel(EasyCodeAcademy.Web.Models.EasyCodeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CateId"] = new SelectList(_context.categories, "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Topic Topic { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.topics == null || Topic == null)
            {
                ViewData["CateId"] = new SelectList(_context.categories, "CategoryId", "CategoryName");
                return Page();
            }

            _context.topics.Add(Topic);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
