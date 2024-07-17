using API.Services;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/moderator")]
    [ApiController]
    public class ModeratorsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private IMailService _mailService;
        private readonly ITutorService _tutorService;
        private readonly IComplaintService _complaintService;

        public ModeratorsController(IAccountService accountService, IMailService mailService, ITutorService tutorService, IComplaintService complaintService)
        {
            _accountService = accountService;
            _mailService = mailService;
            _tutorService = tutorService;
            _complaintService = complaintService;
        }

        [HttpGet("get_tutors")]
        public async Task<IActionResult> ShowListTutorInter()
        {
            var result = await _accountService.GetAccountTutorIsActiveFalse();
            return Ok(result);
        }

        [HttpGet("create_email")]
        public async Task<IActionResult> SendEmailInterTutor(string email, string content)
        {
            if (await _accountService.CheckAccountByEmail(email))
            {
                _mailService.SendTutorInter(email, "Phỏng vấn On Demand Tutor", content);
                return Ok();
            }
            return BadRequest("No Account");
        }

        [HttpPost("update_status")]
        public async Task<IActionResult> ChangeStatusTutor(List<IsActiveTutor> acccount)
        {
            var listResult = new List<string>();
            foreach (var x in acccount)
            {
                if (await _tutorService.ChangeStatusTutor(x))
                {
                    listResult.Add(x.AccountId);
                }
            }
            return Ok(listResult);
        }

        [HttpPut("get_complaint-detail")]
        public async Task<IActionResult> ModerComplaint (string complaintId, string pro, bool stu)
        {
            var result = await _complaintService.ModeratorComplaint(complaintId, pro, stu);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("get_complaints")]
        public async Task<IActionResult> ShowListComplaint()
        {
            var list = await _complaintService.ShowListComplaintClass();
            return Ok(list);
        }
    }
}
