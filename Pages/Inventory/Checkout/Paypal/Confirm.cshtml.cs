using BraintreeHttp;
using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PayPal.Core;
using PayPal.v1.Payments;
using EasyCodeAcademy.Web.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace EasyCodeAcademy.Web.Pages.Inventory.Checkout.Paypal
{
    [Authorize]
    public class ConfirmModel : PageModel
    {
        private readonly string _clientId;
        private readonly string _secetKey;

        public string paymentId { get; set; } = default!;
        public string PayerID { get; set; } = default!;
        public string token { get; set; } = default!;

        public ConfirmModel(IConfiguration config)
        {
            _clientId = config["PaypalSettings:ClientId"];
            _secetKey = config["PaypalSettings:SecretKey"];
        }

        public decimal total { get; set; } = 0;

        public IActionResult OnGet()
        {
            // Get Payment Data From Paypal After Paypal Return
            var requestQuery = Request.Query;

            if (requestQuery != null)
            {
                paymentId = requestQuery["paymentId"];

                PayerID = requestQuery["PayerID"];

                token = requestQuery["token"];

                if (paymentId != null && PayerID != null && token != null)
                {
                    // Get Total Amount Of Inventory
                    List<CartItem> cartItems = GetCartItems();

                    foreach (var cartitem in cartItems)
                    {
                        var thanhtien = cartitem.quantity * cartitem.course.CoursePrice;
                        total += thanhtien;
                    }

                    return Page();
                }
            }

            return NotFound($"Checkout Session Not Found");
        }

        public async Task<IActionResult> OnPostAsync(string? paymentId, string? PayerID, string? token)
        {
            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var environment = new SandboxEnvironment(_clientId, _secetKey);
            var client = new PayPalHttpClient(environment);

            if (paymentId != null && PayerID != null && token != null)
            {
                PaymentExecuteRequest request = new PaymentExecuteRequest(paymentId);
                var paymentExecution = new PaymentExecution()
                {
                    PayerId = PayerID,
                };
                request.RequestBody(paymentExecution);
                try
                {
                    await client.Execute(request);
                    return Redirect($"{hostname}/Inventory/PaypalCheckout/Success?paymentId={paymentId}");
                }
                catch (HttpException httpException)
                {
                    var statusCode = httpException.StatusCode;
                    var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                    //Process when Checkout with Paypal fails
                    //return Redirect("/Paypal/CheckoutFail");
                    return Redirect($"{hostname}/Inventory/PaypalCheckout/Failed?paymentId={paymentId}");
                }
            }

            return Redirect($"{hostname}/Inventory/PaypalCheckout/Failed?paymentId={paymentId}");
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
