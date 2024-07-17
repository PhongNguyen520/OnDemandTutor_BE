using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.PaymentServices;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MomoController : ControllerBase
    {
        private readonly MomoService _momoService;

        public MomoController()
        {
            _momoService = new MomoService();
        }

        [HttpPost("create_url")]
        public async Task<IActionResult> CreatePayment([FromBody] MomoRequest request)
        {
            try
            {
                var orderUrl = await _momoService.CreateOrderAsync(request.Amount, request.Description, request.orderId);
                return Ok(new { orderUrl });
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

    }

}
