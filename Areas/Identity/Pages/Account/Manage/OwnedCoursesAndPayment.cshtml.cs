using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EasyCodeAcademy.Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class OwnedCoursesAndPaymentModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly EasyCodeContext _context;

        public OwnedCoursesAndPaymentModel(
            EasyCodeContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IList<ECAPayment> ECAPayments { get; set; } = default!;

        public IList<Course> Courses { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ECAPayments != null && _context.courses != null)
            {
                ECAPayments = await _context.ECAPayments.Where(u => u.UserId == _userManager.GetUserId(User).ToString())
                                                        .ToListAsync();
                Courses = await _context.courses.ToListAsync();
            }
        }
    }
}
