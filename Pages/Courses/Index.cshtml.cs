using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static EasyCodeAcademy.Web.Pages.Inventory.IndexModel;

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

        public IList<Course> Course { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var categories = (from c in easyCodeContext.categories
                              orderby c.created_date descending
                              select c);
            Category = await categories.Include(c => c.Topics).ToListAsync();

            var courses = (from c in easyCodeContext.courses
                           orderby c.created_date descending
                           select c);
            Course = await courses.ToListAsync();
        }
    }
}
