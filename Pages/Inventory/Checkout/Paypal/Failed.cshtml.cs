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
    public class FailedModel : PageModel
    {
        private readonly EasyCodeContext _context;
        private readonly UserManager<AppUser> _userManager;

        public FailedModel(EasyCodeContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string? paymentId, string? statusCode, string? debugId, string? token)
        {
            // Cancel Confirm Paypal Payment
            if(paymentId != null)
            {
                var inventory = GetCartItems();
                if (inventory != null && inventory.Count > 0)
                {
                    foreach (var inventoryItem in inventory)
                    {
                        ECAPayment ecaPayment = new ECAPayment();
                        ecaPayment.paymentId = paymentId;
                        ecaPayment.Method = "Paypal";
                        ecaPayment.Status = 0; // 1 = success, 0 = false
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
                }
                return Page();
            }
            // Cancel Paypal Payment
            else if (token!= null)
            {
                return Page();
            }
            // Error Occur When Checkout Paypal Error Inside Web Page
            else if (statusCode != null || debugId != null)
            {
                return Page();
            }
            return NotFound("Checkout Session Not Found.");
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
    }
}
