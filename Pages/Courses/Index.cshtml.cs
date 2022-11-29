using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PayPal.v1.Payments;
using static EasyCodeAcademy.Web.Areas.Admin.Pages.User.IndexModel;
using static EasyCodeAcademy.Web.Pages.Inventory.IndexModel;

namespace EasyCodeAcademy.Web.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly EasyCodeContext easyCodeContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IndexModel(
            EasyCodeContext _easyCodeContext,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            easyCodeContext = _easyCodeContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IList<Category> Category { get; set; } = default!;

        public IList<Course> Course { get; set; } = default!;

        public IList<ECAPayment> ECAPayments { get; set; } = default!;

        public string FilterQuery { get; set; } = default!;

        public string SearchQuery { get; set; } = default!;

        public string TopicName { get; set; } = default!;

        public int? TopicId { get; set; }

        public const int ITEMS_PER_PAGE = 10;

        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }

        public int countPages { get; set; }

        public async Task OnGetAsync(int? topicId, string? SearchString, string? filterBy)
        {
            var categories = (from c in easyCodeContext.categories
                              orderby c.created_date descending
                              select c);
            Category = await categories.Include(c => c.Topics).ToListAsync();

            var courses = (from c in easyCodeContext.courses
                           orderby c.created_date descending
                           select c);

            if (topicId != null)
            {
                var topic = await easyCodeContext.topics.FirstOrDefaultAsync(t => t.TopicId == topicId);

                if(topic != null)
                {
                    TopicName = topic.TopicName;
                    TopicId = topic.TopicId;
                }

                Course = await courses.Where(t => t.TopicId == topicId).ToListAsync();
            } 
            else if(SearchString != null)
            {
                SearchQuery = SearchString;
                Course = await courses.Where(t => t.CourseName.Contains(SearchString)).ToListAsync();
            }
            else if(filterBy != null)
            {
                FilterQuery = filterBy;
                if (filterBy.Contains("Price"))
                {
                    Course = await courses.OrderBy(c => c.created_date).ToListAsync();
                }
                else if (filterBy.Contains("Latest"))
                {
                    Course = await courses.OrderBy(p => p.CoursePrice).ToListAsync();
                }
                else if(filterBy.Contains("Name"))
                {
                    Course = await courses.OrderBy(n => n.CourseName).ToListAsync();
                }
            }
            else
            {
                Course = await courses.ToListAsync();
            }

            if(_userManager.GetUserId(User) != null)
            {
                ECAPayments = await easyCodeContext.ECAPayments.Where(u => u.UserId == _userManager.GetUserId(User).ToString())
                                                .ToListAsync();
            }

            // Paging
            if(Course != null)
            {
                int totalCourse = Course.Count;
                countPages = (int)Math.Ceiling((double)totalCourse / ITEMS_PER_PAGE);

                if (currentPage < 1)
                {
                    currentPage = 1;
                }
                if (currentPage > countPages)
                {
                    currentPage = countPages;
                }

                var qr1 = Course.Skip((currentPage - 1) * ITEMS_PER_PAGE)
                            .Take(ITEMS_PER_PAGE);

                Course = qr1.ToList();
            }
        }
    }
}
