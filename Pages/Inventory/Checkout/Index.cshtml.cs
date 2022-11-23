using EasyCodeAcademy.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PayPal.Core;
using PayPal.v1.Payments;
using EasyCodeAcademy.Web.Helpers;
using Newtonsoft.Json;
using BraintreeHttp;

namespace EasyCodeAcademy.Web.Pages.Inventory.Checkout
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly string _clientId;
        private readonly string _secetKey;
        private readonly EasyCodeContext _context;

        // Exchange Rate From USD -> VietNam Dong (Store In Database)
        public double ExchangeRateUSD = 23300;

        public IndexModel(EasyCodeContext context, IConfiguration config)
        {
            _context = context;
            _clientId = config["PaypalSettings:ClientId"];
            _secetKey = config["PaypalSettings:SecretKey"];
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var environment = new SandboxEnvironment(_clientId, _secetKey);
            var client = new PayPalHttpClient(environment);
            List<CartItem> cartItems = GetCartItems();
            decimal total = 0;

            foreach (var cartitem in cartItems)
            {
                var thanhtien = cartitem.quantity * cartitem.course.CoursePrice;
                total += thanhtien;
            }

            #region Create Paypal Order
            var itemList = new ItemList()
            {
                Items = new List<Item>()
            };
            //var total = cartItems.Sum(p => p.);
            foreach (var item in cartItems)
            {
                itemList.Items.Add(new Item()
                {
                    Name = item.course.CourseName,
                    Currency = "USD",
                    Price = item.course.CoursePrice.ToString("n2"),
                    Quantity = item.quantity.ToString(),
                    Sku = "sku",
                    Tax = "0"
                });
            }
            #endregion

            var paypalOrderId = DateTime.Now.Ticks;
            var hostname = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

            var payment = new Payment()
            {
                Intent = "sale",
                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = total.ToString("n2"),
                            Currency = "USD",
                            Details = new AmountDetails
                            {
                                Tax = "0",
                                Shipping = "0",
                                Subtotal = total.ToString("n2")
                            }
                        },
                        ItemList = itemList,
                        Description = $"Invoice #{paypalOrderId}",
                        InvoiceNumber = paypalOrderId.ToString()
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    //CancelUrl = "https://localhost:5001/Inventory/PaypalCheckout/Failed",
                    //ReturnUrl = "https://localhost:5001/Inventory/PaypalCheckout/Success"
                    CancelUrl = $"{hostname}/Inventory/PaypalCheckout/Failed",
                    ReturnUrl = $"{hostname}/Inventory/PaypalCheckout/Confirm"
                },
                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                var response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();

                var links = result.Links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    LinkDescriptionObject lnk = links.Current;
                    if (lnk.Rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment  
                        paypalRedirectUrl = lnk.Href;
                    }
                }

                return Redirect(paypalRedirectUrl);
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
