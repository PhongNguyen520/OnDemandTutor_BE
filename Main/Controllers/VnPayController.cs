using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        private readonly string vnp_TmnCode = "WX3Z3JAI";
        private readonly string vnp_HashSecret = "TI56EMVNTQM55D9JT08D93N4N825CE05";
        private readonly string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private readonly string vnp_ReturnUrl = "http://localhost:3000/payment-return";

        [HttpPost("create_payment_url")]
        public IActionResult CreatePaymentUrl([FromBody] PaymentRequest request)
        {
            var date = DateTime.Now;
            var createDate = date.ToString("yyyyMMddHHmmss");
            var orderInfo = request.Description;
            var orderType = "other";
            var locale = "vn";
            var currCode = "VND";
            var vnp_Params = new SortedDictionary<string, string>
            {
                { "vnp_Version", "2.1.0" },
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", vnp_TmnCode },
                { "vnp_Locale", locale },
                { "vnp_CurrCode", currCode },
                { "vnp_TxnRef", request.PaymentId },
                { "vnp_OrderInfo", orderInfo },
                { "vnp_OrderType", orderType },
                { "vnp_Amount", (request.Amount * 100).ToString() },
                { "vnp_ReturnUrl", vnp_ReturnUrl },
                { "vnp_IpAddr", HttpContext.Connection.RemoteIpAddress.ToString() },
                { "vnp_CreateDate", createDate }
            };

            var querystring = string.Join("&", vnp_Params.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

            var signData = HmacSHA512(vnp_HashSecret, querystring);

            var paymentUrl = $"{vnp_Url}?{querystring}&vnp_SecureHash={signData}";

            return Ok(new { paymentUrl, vnp_Params });
        }

        private static string HmacSHA512(string key, string data)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }

    public class PaymentRequest
    {
        public string PaymentId { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
    }

}
