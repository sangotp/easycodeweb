using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public CreateModel(EasyCodeContext _easyCodeContext)
        {
            easyCodeContext = _easyCodeContext;
        }

        public void OnGet()
        {
        }


        public IActionResult OnPost(int? courseid)
        {
            var course = easyCodeContext.courses
                                  .Where(p => p.CourseId == courseid)
                                  .FirstOrDefault();
            if (course == null)
                return NotFound("Course Not Found");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.course.CourseId == courseid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
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