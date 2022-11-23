using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace EasyCodeAcademy.Web.Pages_Manage_Categories
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;

        public IndexModel(EasyCodeAcademy.Web.Models.EasyCodeContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync(string SearchString)
        {
            if (_context.categories != null)
            {
                var categories = from c in _context.categories
                                 orderby c.created_date descending
                                 select c;

                if (!string.IsNullOrEmpty(SearchString))
                {
                    Category = await categories.Where(c => c.CategoryName.Contains(SearchString)).ToListAsync();
                }
                else
                {
                    //Category = await _context.categories.ToListAsync();
                    Category = await categories.ToListAsync();
                }
            }
        }
    }
}
