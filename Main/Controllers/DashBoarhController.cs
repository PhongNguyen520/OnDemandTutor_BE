using API.Services;
using BusinessObjects.Constrant;
using BusinessObjects.Models;
using BusinessObjects.Models.TutorModel;
using Microsoft.AspNet.SignalR;
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
        private readonly IPaymentTransactionService _paymentTransactionService;
        public DashBoarhController(IAccountService accountService, ICurrentUserService currentUserService)
        {
            _iAccountService = accountService;
            _currentUserSrevice = currentUserService;
            _tutorService = new TutorService();
            _paymentTransactionService = new PaymentTransactionService();
        }

        [HttpGet("get_accounts")]
        public async Task<IActionResult> ShowAccountIsActive()
        {
            var result = await _iAccountService.ListAccountIsActive();
            return Ok(result);
        }

        [Authorize(Roles = AppRole.Tutor)]
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

        [Authorize(Roles = AppRole.Admin)]
        [HttpGet("admin_dashboard")]
        public async Task<IActionResult> AdminDashBoard()
        {
            var user = _currentUserSrevice.GetUserId();
            var account = _iAccountService.GetAccounts().Where(s => s.Id == user.ToString()).FirstOrDefault();
            List<DashBoardAdmin> dashBoardAdmins = new List<DashBoardAdmin>();
            dashBoardAdmins.Add(await _paymentTransactionService.GetDashBoard(account.Id, account.CreateDay, 1, "Total amount received"));
            dashBoardAdmins.Add(await _paymentTransactionService.GetDashBoard(account.Id, account.CreateDay, 2, "Total amount withdrawn by the tutor"));
            dashBoardAdmins.Add(await _paymentTransactionService.GetDashBoard(account.Id, account.CreateDay, 3, "Total refund amount"));
            return Ok(dashBoardAdmins.ToList());
        }
    }
}
