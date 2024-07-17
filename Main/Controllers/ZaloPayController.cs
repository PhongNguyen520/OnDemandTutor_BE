using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZaloPayController : ControllerBase
    {

        private readonly ZaloPayService _zaloPayService;

        public ZaloPayController()
        {
            _zaloPayService = new ZaloPayService();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)
        {
            try
            {
                var orderUrl = await _zaloPayService.CreateOrderAsync(request.Amount, request.Description);
                return Ok(new { orderUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
