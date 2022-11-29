using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EasyCodeAcademy.Web.Pages.Inventory
{
    public class CreateModel : PageModel
    {
        public class CartItem
        {
            public int quantity { set; get; }
            public Course? course { set; get; }
        }

        private readonly EasyCodeContext easyCodeContext;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public CreateModel(
            EasyCodeContext _easyCodeContext,
            SignInManager<AppUser> signInManager, 
            UserManager<AppUser> userManager)
        {
            easyCodeContext = _easyCodeContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        private IList<ECAPayment> ECAPayments { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(int? courseid, string? returnUrl)
        {
            var course = easyCodeContext.courses
                                  .Where(p => p.CourseId == courseid)
                                  .FirstOrDefault();
            if (course == null)
                return NotFound("Course Not Found");

            bool isOwned = false;

            if (_signInManager.IsSignedIn(User) && isOwned == false)
            {
                // Xử lý đưa vào Cart ...
                var cart = GetCartItems();
                var cartitem = cart.Find(p => p.course.CourseId == courseid);
                if (cartitem != null)
                {
                    // Đã tồn tại, tăng thêm 1
                    //cartitem.quantity++;
                }
                else
                {
                    //  Thêm mới
                    cart.Add(new CartItem() { quantity = 1, course = course });
                }

                // Lưu cart vào Session
                SaveCartSession(cart);
                // Chuyển đến trang hiện thị Cart
                return Redirect("/Inventory");
            }
            else if (_signInManager.IsSignedIn(User) && isOwned)
            {
                return NotFound("You already own this course. Unable to add.");
            }
            else
            {
                if (returnUrl != null)
                {
                    string returnUrlQuery = $"?ReturnUrl={Url.Content("~" + returnUrl)}";
                    return LocalRedirect($"/Identity/Account/Login{returnUrlQuery}");
                }
                else
                {
                    string returnUrlQuery = $"?ReturnUrl={Url.Content("~/")}";
                    return LocalRedirect($"/Identity/Account/Login{returnUrlQuery}");
                }
            }

            ECAPayments = await easyCodeContext.ECAPayments.Where(u => u.UserId == _userManager.GetUserId(User).ToString()).ToListAsync();

            if(ECAPayments != null)
            {
                foreach(var payment in ECAPayments)
                {
                    if(payment.courseId == courseid && payment.Status == 1)
                    {
                        isOwned = true;
                        break;
                    }
                }
            }
        }

        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}