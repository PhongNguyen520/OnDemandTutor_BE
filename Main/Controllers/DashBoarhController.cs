using API.Services;
using BusinessObjects.Models.TutorModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashBoarhController : ControllerBase
    {
        private readonly IAccountService _iAccountService;
        private readonly ICurrentUserService _currentUserSrevice;
        private readonly ITutorService _tutorService;
        public DashBoarhController(IAccountService accountService, ICurrentUserService currentUserService)
        {
            _iAccountService = accountService;
            _currentUserSrevice = currentUserService;
            _tutorService = new TutorService();
        }

        [HttpGet("get_accounts")]
        public async Task<IActionResult> ShowAccountIsActive()
        {
            var result = await _iAccountService.ListAccountIsActive();
            return Ok(result);
        }

        [HttpGet("tutor_dashboard")]
        public async Task<IActionResult> TutorDashBoard()
        {
            var user = _currentUserSrevice.GetUserId();
            var account = _iAccountService.GetAccounts().Where(s => s.Id == user.ToString()).FirstOrDefault();
            List<DashBoardTutor> dashBoardTutors = new List<DashBoardTutor>();
            dashBoardTutors.Add(await _tutorService.NumberOfClasses(account.Id, account.CreateDay));
            dashBoardTutors.Add(await _tutorService.NumberOfHour(account.Id, account.CreateDay));
            dashBoardTutors.Add(await _tutorService.NumberOfClassesIsCancel(account.Id, account.CreateDay));

            return Ok(dashBoardTutors.ToList());
        }
    }
}
