using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoarhController : ControllerBase
    {
        private readonly IAccountService _iAccountService;
        public DashBoarhController(IAccountService accountService)
        {
            _iAccountService = accountService;
        }

        [HttpGet("ShowListAcount")]
        public async Task<IActionResult> ShowAccountIsActive()
        {
            var result = await _iAccountService.ListAccountIsActive();
            return Ok(result);
        }
    }
}
