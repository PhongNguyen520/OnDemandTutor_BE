using API.Helpers;
using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
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

    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        private readonly VnPayService vnPayService;
        private readonly IPaymentService paymentService;
        private readonly IPaymentTransactionService transactionService;
        private readonly IWalletService walletService;
        private readonly string vnp_TmnCode = "WX3Z3JAI";
        private readonly string vnp_HashSecret = "TI56EMVNTQM55D9JT08D93N4N825CE05";
        private readonly string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private readonly string returnUrl = "http://localhost:3000/notifications";

        public VnPayController()
        {
            vnPayService = new VnPayService();
            paymentService = new PaymentService();
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
            var paymentId = Guid.NewGuid().ToString();

            var payment = new Payment()
            {
                Id = paymentId,
                RequiredAmount = request.Amount,
                Description = request.Description,
                CurrencyCode = currCode,
                TxnRef = txnRef,
                Signature = signature,
                Status = true,
                WalletId = request.WalletId,
                PaymentDestinationId = request.PaymentDestinationId
            };

            paymentService.AddPayment(payment);

            return Ok( new { paymentUrl, paymentId });
        }

        [HttpPost]
        [Route("payment_return/{id}")]
        public IActionResult PaymentReturn(string id, [FromBody] ResponsePayment response)
        {
            var requestPayment = paymentService.GetPayments().FirstOrDefault(s => s.TxnRef == response.vnp_TxnRef);


            if (requestPayment != null)
            {
                var transaction = new PaymentTransaction()
                {
                    Id = Guid.NewGuid().ToString(),
                    Amount = response.vnp_Amount,
                    Description = response.vnp_OrderInfo,
                    CardType = response.vnp_CardType,
                    TxnRef = response.vnp_TxnRef,
                    BankTranNo = response.vnp_BankTranNo,
                    TranStatus = response.vnp_TransactionStatus,
                    ResponseCode = response.vnp_ResponseCode,
                    IsValid = true,
                    PaymentId = id
                };

                if (response.vnp_ResponseCode == "00")
                {
                    transactionService.AddTransaction(transaction);

                    var payment = paymentService.GetPayments().FirstOrDefault(p => p.Id == id);
                   if (payment != null)
                    {
                        var wallet = walletService.GetWallets().FirstOrDefault(w => w.WalletId == payment.WalletId);
                        wallet.Balance = wallet.Balance + response.vnp_Amount;
                        walletService.UpdateWallets(wallet);
                    }
                }
                else
                {
                    transaction.IsValid = false;
                    transactionService.AddTransaction(transaction);
                }
                    return Ok(response.vnp_ResponseCode);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
