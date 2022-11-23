using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EasyCodeAcademy.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EasyCodeAcademy.Web.Pages.Manage
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly EasyCodeContext _context;

        public IndexModel(EasyCodeContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if(_context.categories != null)
            {
                var categories = from c in _context.categories
                                 orderby c.created_date descending
                                 select c;
                Category = await categories.ToListAsync();
            }
        }
    }
}
