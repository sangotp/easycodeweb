using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EasyCodeAcademy.Web.Models;

namespace EasyCodeAcademy.Web.Pages_Manage_Topics
{
    public class DetailsModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;

        public DetailsModel(EasyCodeAcademy.Web.Models.EasyCodeContext context)
        {
            _context = context;
        }

      public Topic Topic { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.topics == null)
            {
                return NotFound();
            }

            var topic = await _context.topics.Include(t => t.Category).FirstOrDefaultAsync(m => m.TopicId == id);
            if (topic == null)
            {
                return NotFound();
            }
            else 
            {
                Topic = topic;
            }
            return Page();
        }
    }
}
