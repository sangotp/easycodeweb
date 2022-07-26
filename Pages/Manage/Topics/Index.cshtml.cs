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
    public class IndexModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;

        public IndexModel(EasyCodeAcademy.Web.Models.EasyCodeContext context)
        {
            _context = context;
        }

        public IList<Topic> Topic { get;set; } = default!;

        public async Task OnGetAsync(string SearchString)
        {
            //if (_context.topics != null)
            //{
            //    Topic = await _context.topics
            //    .Include(t => t.Category).ToListAsync();
            //}

            if (_context.topics != null)
            {
                var topics= from t in _context.topics
                                 orderby t.created_date descending
                                 select t;

                if (!string.IsNullOrEmpty(SearchString))
                {
                    Topic = await topics.Include(t => t.Category).Where(t => t.TopicName.Contains(SearchString)).ToListAsync();
                }
                else
                {
                    //Category = await _context.categories.ToListAsync();
                    Topic = await topics.Include(t => t.Category).ToListAsync();
                }
            }
        }
    }
}
