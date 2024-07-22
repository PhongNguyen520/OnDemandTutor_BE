using API.Helpers;
using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Org.BouncyCastle.Asn1.Ocsp;
using Services;
using Services.PaymentServices;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{

    [Route("api/vnpay")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        private readonly VnPayService vnPayService;
        private readonly IPaymentTransactionService transactionService;
        private readonly IWalletService walletService;
        private readonly string vnp_TmnCode = "WX3Z3JAI";
        private readonly string vnp_HashSecret = "TI56EMVNTQM55D9JT08D93N4N825CE05";
        private readonly string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private readonly string returnUrl = "http://localhost:3000/classes";

        public VnPayController()
        {
            vnPayService = new VnPayService();
            transactionService = new PaymentTransactionService();
            walletService = new WalletService();
        }

        [HttpPost]
        [Route("create_payment_url")]
        public IActionResult CreatePaymentUrl([FromBody] RequestPayment request)
        {
            var orderType = "other";
            var locale = "vn";
            var currCode = "VND";
            var txnRef = Guid.NewGuid().ToString();
            var createDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            var vnp_Params = new SortedDictionary<string, string>
            {
                { "vnp_Version", "2.1.0" },
                { "vnp_Command", "pay" },
                { "vnp_TmnCode", vnp_TmnCode },
                { "vnp_Locale", locale },
                { "vnp_CurrCode", currCode },
                { "vnp_TxnRef", txnRef },
                { "vnp_OrderInfo", request.Description },
                { "vnp_OrderType", orderType },
                { "vnp_Amount", (request.Amount * 100).ToString() },
                { "vnp_ReturnUrl", returnUrl },
                { "vnp_IpAddr", HttpContext.Connection.RemoteIpAddress.ToString() },
                { "vnp_CreateDate", createDate },
                {"vnp_ExpireDate", DateTime.Now.AddMinutes(30).ToString("yyyyMMddHHmmss") },
            };

            var paymentUrl = vnPayService.CreateRequestUrl(vnp_Url, vnp_HashSecret, vnp_Params);
            var signature = vnPayService.CreateSignature(vnp_HashSecret, vnp_Params);
            var transactionId = Guid.NewGuid().ToString();

            var payment = new PaymentTransaction()
            {
                Id = transactionId,
                Amount = request.Amount,
                Description = request.Description,
                TranDate = DateTime.Now,
                Type = request.Type,
                IsValid = false,
                WalletId = request.WalletId,
                PaymentDestinationId = request.PaymentDestinationId
            };

            transactionService.AddTransaction(payment);

            return Ok(new { paymentUrl, transactionId });
        }

        [HttpPost]
        [Route("payment_return/{id}")]
        public IActionResult PaymentReturn(string id, [FromBody] ResponsePayment response)
        {
            var vnp_Params = new SortedDictionary<string, string>
            {
                { "vnp_Amount", response.vnp_Amount.ToString() },
                { "vnp_BankCode", response.vnp_BankCode },
                { "vnp_BankTranNo", response.vnp_BankTranNo },
                { "vnp_CardType", response.vnp_CardType },
                { "vnp_OrderInfo", response.vnp_OrderInfo },
                { "vnp_PayDate", response.vnp_PayDate },
                { "vnp_ResponseCode", response.vnp_ResponseCode },
                { "vnp_TmnCode", response.vnp_TmnCode },
                { "vnp_TransactionNo", response.vnp_TransactionNo },
                { "vnp_TxnRef", response.vnp_TxnRef },
                { "vnp_TransactionStatus", response.vnp_TransactionStatus }
            };

            var secureHash = vnPayService.CreateSignature(vnp_HashSecret, vnp_Params);

            if (secureHash != response.vnp_SecureHash)
            {
                return BadRequest("Invalid signature");
            }

            var payment = transactionService.GetTransactions().FirstOrDefault(s => s.Id == id);
            if (payment != null)
            {
                var amount = response.vnp_Amount / 100;

                if (response.vnp_ResponseCode == "00")
                {
                    payment.IsValid = true;
                    transactionService.UpdateTransaction(payment);
                    var wallet = walletService.GetWallets().FirstOrDefault(w => w.WalletId == payment.WalletId);
                    wallet.Balance = wallet.Balance + amount;
                    walletService.UpdateWallets(wallet);
                }
                else
                {
                    return Ok(false);
                }

            }
            return Ok(true);

        }
    }
}
