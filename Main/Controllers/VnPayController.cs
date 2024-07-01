using API.Helpers;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{

    [Route("api/vnpay")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        private readonly string vnp_TmnCode = "WX3Z3JAI";
        private readonly string vnp_HashSecret = "TI56EMVNTQM55D9JT08D93N4N825CE05";
        private readonly string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private readonly string vnp_ReturnUrl = "http://localhost:3000/notifications";

        [HttpPost("create_payment_url")]
        public IActionResult CreatePaymentUrl([FromBody] VnPayVM request)
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
                { "vnp_TxnRef", Guid.NewGuid().ToString() },
                { "vnp_OrderInfo", orderInfo },
                { "vnp_OrderType", orderType },
                { "vnp_Amount", (request.Amount * 100).ToString() },
                { "vnp_ReturnUrl", vnp_ReturnUrl },
                { "vnp_IpAddr", HttpContext.Connection.RemoteIpAddress.ToString() },
                { "vnp_CreateDate", createDate }
            };

            var querystring = string.Join("&", vnp_Params.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

            var signData = HashHelper.HmacSHA256(vnp_HashSecret, querystring);

            var paymentUrl = $"{vnp_Url}?{querystring}&vnp_SecureHash={signData}";

            return Ok(new { paymentUrl, vnp_Params });
        }

    }

}
