using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EasyCodeAcademy.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EasyCodeContext _context;

        public IndexModel(EasyCodeContext context)
        {
            _context = context;
        }

        public IList<Topic> Topics { get; set; } = default!;

        public async Task OnGet()
        {
            ViewData["Title"] = "EasyCode Academy - Home";

            var topics = await _context.topics.ToListAsync();
            if(topics != null)
            {
                Topics = topics;
            }
        }
    }
}
