using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeratorsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private IMailService _mailService;
        private readonly ITutorService _tutorService;

        public ModeratorsController(IAccountService accountService, IMailService mailService, ITutorService tutorService)
        {
            _accountService = accountService;
            _mailService = mailService;
            _tutorService = tutorService;
        }

        [HttpGet("ShowListTutorInter")]
        public async Task<IActionResult> ShowListTutorInter()
        {
            var result = await _accountService.GetAccountTutorIsActiveFalse();
            return Ok(result);
        }

        [HttpGet("SendEmailInterTutor")]
        public async Task<IActionResult> SendEmailInterTutor(string email, string content)
        {
            if (await _accountService.CheckAccountByEmail(email))
            {
                _mailService.SendTutorInter(email, "Phỏng vấn On Demand Tutor", content);
                return Ok();
            }
            return BadRequest("No Account");
        }

        [HttpPost("ChangeStatusTutor")]
        public async Task<IActionResult> ChangeStatusTutor(string idAccount)
        {
            if (await _tutorService.ChangeStatusTutor(idAccount))
            {
                return Ok("Successful");
            }
            return BadRequest("No Found!!!");
        }
    }
}
