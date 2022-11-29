using BraintreeHttp;
using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PayPal.Core;
using PayPal.v1.Payments;

namespace EasyCodeAcademy.Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class PaymentHistoryModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly EasyCodeContext _context;
        private readonly string _clientId;
        private readonly string _secetKey;

        public PaymentHistoryModel(
            EasyCodeContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _clientId = config["PaypalSettings:ClientId"];
            _secetKey = config["PaypalSettings:SecretKey"];
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IList<ECAPayment> ECAPayments { get; set; } = default!;

        public Object PaypalPaymentDetails { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.ECAPayments != null)
            {
                ECAPayments = await _context.ECAPayments.Where(u => u.UserId == _userManager.GetUserId(User).ToString())
                                                        .ToListAsync();
            }
        }

        public async Task<IActionResult> OnGetPaypalPaymentDetails(string? paymentId)
        {
            if(paymentId == null)
            {
                return NotFound("Paypal Payment Details Not Found.");
            }

            var environment = new SandboxEnvironment(_clientId, _secetKey);
            var client = new PayPalHttpClient(environment);

            PaymentGetRequest request = new PaymentGetRequest(paymentId);

            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = $"{hostname}/Identity/Account/Manage/PaymentHistory";

                return new JsonResult(response.Result<Payment>());
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();

                //Process when Checkout with Paypal fails
                //return Redirect("/Paypal/CheckoutFail");
                return Redirect($"{hostname}/Inventory/PaypalCheckout/Failed?statusCode={statusCode}&debugId={debugId}");
            }
        }
    }
}
