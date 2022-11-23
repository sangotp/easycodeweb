using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace EasyCodeAcademy.Web.Pages.Inventory
{
    public class EditModel : PageModel
    {
        public class CartItem
        {
            public int quantity { set; get; }
            public Course? course { set; get; }
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(int? courseid, int? quantity)
        {
            if(courseid != null && quantity != null)
            {
                // Cập nhật Cart thay đổi số lượng quantity ...
                var cart = GetCartItems();
                var cartitem = cart.Find(p => p.course.CourseId == courseid);
                if (cartitem != null)
                {
                    // Đã tồn tại, tăng thêm 1
                    cartitem.quantity = (int)quantity;
                }
                SaveCartSession(cart);
            } else
            {
                return NotFound();
            }
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return StatusCode(StatusCodes.Status200OK);
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
