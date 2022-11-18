using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EasyCodeAcademy.Web.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "EasyCode Academy - Home";
        }
    }
}
