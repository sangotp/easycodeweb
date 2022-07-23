using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EasyCodeAcademy.Web.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly EasyCodeContext easyCodeContext;

        public IndexModel(EasyCodeContext _easyCodeContext)
        {
            easyCodeContext = _easyCodeContext;
        }

        public void OnGet()
        {
            var categories = (from c in easyCodeContext.categories
                             orderby c.CategoryId descending
                             select c).ToList();
            ViewData["categories"] = categories;
        }
    }
}
