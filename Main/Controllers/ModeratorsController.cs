﻿using API.Services;
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
        private readonly ITutorAdService _tutorAdService;
        private readonly IWalletService _walletService;

        public ModeratorsController(IAccountService accountService, IMailService mailService, ITutorService tutorService, IComplaintService complaintService, ITutorAdService tutorAdService, IWalletService walletService)
        {
            _accountService = accountService;
            _mailService = mailService;
            _tutorService = tutorService;
            _complaintService = complaintService;
            _tutorAdService = tutorAdService;
            _walletService = walletService;
        }

        [HttpGet("get_tutors")]
        public async Task<IActionResult> ShowListTutorInter()
        {
            var result = await _accountService.GetAccountTutorIsActiveFalse();
            return Ok(result);
        }

        [HttpPut("create_email")]
        public async Task<IActionResult> SendEmailInterTutor(HistoryTutorApplyVM models)
        {
            if (await _accountService.CheckAccountByEmail(models.Email))
            {
                models.HistoryTutorApplyId = Guid.NewGuid().ToString();
                var chec = await _tutorService.CreateHistoryTutorApply(models);
               if(chec == false)
                {
                    return BadRequest("Error Create HistoryTutorApply");
                }
                _mailService.SendTutorInter(models.Email, "Phỏng vấn On Demand Tutor", models.Content);

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

        [HttpGet("show_tutorAd_browse")]
        public async Task<IActionResult> ShowTutorAdBrowse()
        {
            var result = await _tutorAdService.GetAllTutorAdIsActive();
            return Ok(result);
        }

        [HttpPost("changeisactivead")]
        public async Task<IActionResult> ChangeIsActiveAd(TutorAdIsAc model)
        {
            var result = await _tutorAdService.UpdateIsActiveTutorAd(model);
            if (result == false)
            {
                return BadRequest("No ADS!!!");
            }
            return Ok(model.IsActive);
        }

        [HttpGet("show_list_tutor_apply")]
        public async Task<IActionResult> ShowListHistoryTuorApply()
        {
            var result = await _tutorService.GetAllStatusHistoryTutorApply();
            return Ok(result);
        }

        [HttpGet("show_list_request_withdraw_money")]
        public async Task<IActionResult> ShowRequestDraw()
        {
            var result = await _walletService.GetRequestWithdraw();
            return Ok(result);
        }

        [HttpPost("change_IsVa_TransactionPay")]
        public async Task<IActionResult> ChangeIsVaRequestDraw(RequestDrawVM model)
        {
            var result = await _walletService.ChangeStatusWallet(model.idTran, model.Status, model.Amount);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
