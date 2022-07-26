using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EasyCodeAcademy.Web.Models;

namespace EasyCodeAcademy.Web.Pages_Manage_Categories
{
    public class DeleteModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;

        public DeleteModel(EasyCodeAcademy.Web.Models.EasyCodeContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }

            var category = await _context.categories.FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }
            else 
            {
                Category = category;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.categories == null)
            {
                return NotFound();
            }
            var category = await _context.categories.FindAsync(id);

            if (category != null)
            {
                Category = category;
                _context.categories.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
