using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using EasyCodeAcademy.Web.Helpers;

namespace EasyCodeAcademy.Web.Pages.Inventory.Checkout.Paypal
{
    [Authorize]
    public class SuccessModel : PageModel
    {
        private readonly EasyCodeContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SuccessModel(EasyCodeContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //private async Task LoadAsync(string? paymentId)
        //{
        //    if(paymentId != null)
        //    {
        //        ECAPayment.paymentId = paymentId;
        //        ECAPayment.Method = "Paypal";
        //        ECAPayment.Status = 1; // 1 = success, 0 = false
        //        ECAPayment.created_date = DateTime.Now;
        //        ECAPayment.updated_date = DateTime.Now;
        //        ECAPayment.UserId = _userManager.GetUserId(User);
                
        //        if(ECAPayment != null)
        //        {
        //            _context.ECAPayments.Add(ECAPayment);
        //            await _context.SaveChangesAsync();
        //        }
        //    }    
        //}

        public async Task<IActionResult> OnGetAsync(string? paymentId)
        {
            if (paymentId == null)
            {
                return NotFound("Checkout Session Not Found.");
            }

            var inventory = GetCartItems();
            if(inventory != null && inventory.Count > 0)
            {
                foreach (var inventoryItem in inventory)
                {
                    ECAPayment ecaPayment = new ECAPayment();
                    ecaPayment.paymentId = paymentId;
                    ecaPayment.Method = "Paypal";
                    ecaPayment.Status = 1; // 1 = success, 0 = false
                    ecaPayment.courseId = inventoryItem.course.CourseId;
                    ecaPayment.created_date = DateTime.Now;
                    ecaPayment.updated_date = DateTime.Now;
                    ecaPayment.UserId = _userManager.GetUserId(User).ToString();

                    if (ecaPayment != null)
                    {
                        _context.ECAPayments.Add(ecaPayment);
                        await _context.SaveChangesAsync();
                    }
                }

                // Clear Inventory
                ClearCart();
            }

            return Page();
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
