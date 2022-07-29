using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EasyCodeAcademy.Web.Models;

namespace EasyCodeAcademy.Web.Pages_Manage_Courses
{
    public class IndexModel : PageModel
    {
        private readonly EasyCodeAcademy.Web.Models.EasyCodeContext _context;

        public IndexModel(EasyCodeAcademy.Web.Models.EasyCodeContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; } = default!;

        public async Task OnGetAsync(string SearchString)
        {
            //if (_context.courses != null)
            //{
            //    Course = await _context.courses
            //    .Include(c => c.Topic).ToListAsync();
            //}

            if (_context.courses != null)
            {
                var courses = from c in _context.courses
                             orderby c.created_date descending
                             select c;

                if (!string.IsNullOrEmpty(SearchString))
                {
                    Course = await courses.Include(c => c.Topic).Where(c => c.CourseName.Contains(SearchString)).ToListAsync();
                }
                else
                {
                    Course = await courses.Include(c => c.Topic).ToListAsync();
                }
            }
        }
    }
}
