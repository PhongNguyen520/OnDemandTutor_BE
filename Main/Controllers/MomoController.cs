using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.PaymentServices;

namespace API.Controllers
{
    [Route("api/momo")]
    [ApiController]
    public class MomoController : ControllerBase
    {
        private readonly MomoService _momoService;
        private readonly IPaymentTransactionService _transactionService;


        public MomoController()
        {
            _momoService = new MomoService();
            _transactionService = new PaymentTransactionService();
        }

        [HttpPost("create_url")]
        public async Task<IActionResult> CreatePayment([FromBody] MomoRequest request)
        {
            try
            {
                var orderUrl = await _momoService.CreateOrderAsync(request.Amount, request.Description, request.orderId, request.WalletId, request.PaymentDestinationId);
             
                return Ok(new { orderUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("payment_return/{id}")]
        public async Task<IActionResult> PaymentReturn(string id, [FromBody] ResponsePaymentMomo response)
        {
            var responseParams = new Dictionary<string, string>
            {
                { "partnerCode", response.PartnerCode },
                { "orderId", response.OrderId },
                { "requestId", response.RequestId },
                { "amount", response.Amount.ToString() },
                { "orderInfo", response.OrderInfo },
                { "orderType", response.OrderType },
                { "transId", response.TransId },
                { "resultCode", response.ResultCode },
                { "message", response.Message },
                { "payType", response.PayType },
                { "responseTime", response.ResponseTime },
                { "extraData", response.ExtraData },
                { "signature", response.Signature }
            };

            try
            {
                var result = await _momoService.PaymentReturnAsync(id, responseParams);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
    public class MomoRequest
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string orderId { get; set; }
        public string? WalletId { get; set; } = string.Empty;
        public string? PaymentDestinationId { get; set; } = string.Empty;
    }

    public class ResponsePaymentMomo
    {
        public string PartnerCode { get; set; }
        public string OrderId { get; set; }
        public string RequestId { get; set; }
        public decimal Amount { get; set; }
        public string OrderInfo { get; set; }
        public string OrderType { get; set; }
        public string TransId { get; set; }
        public string ResultCode { get; set; }
        public string Message { get; set; }
        public string PayType { get; set; }
        public string ResponseTime { get; set; }
        public string ExtraData { get; set; }
        public string Signature { get; set; }
    }

}
