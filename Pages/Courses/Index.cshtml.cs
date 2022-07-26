using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EasyCodeAcademy.Web.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly EasyCodeContext easyCodeContext;

        public IndexModel(EasyCodeContext _easyCodeContext)
        {
            easyCodeContext = _easyCodeContext;
        }

        public IList<Category> Category { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var categories = (from c in easyCodeContext.categories
                             orderby c.CategoryId descending
                             select c);
            Category = await categories.Include(c => c.Topics).ToListAsync();
        }
    }
}
