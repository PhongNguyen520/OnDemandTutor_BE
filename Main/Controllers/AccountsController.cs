using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DAOs;
using Services;
using Microsoft.AspNetCore.DataProtection.Internal;
using API.Services;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService iAccountService;
        private readonly ITutorService _tutorService;
        private ICurrentUserService _currentUserService;
        private readonly UserManager<Account> _userManager;

        public AccountsController(IAccountService accountService, ITutorService tutorService, ICurrentUserService currentUserService, UserManager<Account> userManager)
        {
            iAccountService = accountService;
            _tutorService = tutorService;
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        // GET: api/Accounts
        [HttpGet]
        public IActionResult GetAccounts()
        {
            return Ok(iAccountService.GetAccounts());
        }

        // GET: api/Accounts/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Account>> GetAccount(string id)
        //{
        //    var account = await _context.Accounts.FindAsync(id);

        //    if (account == null)
        //    {
        //        return NotFound();
        //    }

        //    return account;
        //}

        //// PUT: api/Accounts/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAccount(string id, Account account)
        //{
        //    if (id != account.AccountId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(account).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AccountExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Accounts
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Account>> PostAccount(Account account)
        //{
        //    _context.Accounts.Add(account);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (AccountExists(account.AccountId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetAccount", new { id = account.AccountId }, account);
        //}

        //// DELETE: api/Accounts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteAccount(string id)
        //{
        //    var account = await _context.Accounts.FindAsync(id);
        //    if (account == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Accounts.Remove(account);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool AccountExists(string id)
        //{
        //    return _context.Accounts.Any(e => e.AccountId == id);
        //}

        [HttpGet("show_10-tutor-new")]
        public async Task<IActionResult> Show10TutorNew()
        {
            var list = await iAccountService.Get10TutorNew();
            return Ok(list);
        }

        [HttpGet("show_10-student-new")]
        public async Task<IActionResult> Show10StudentNew()
        {
            var list = await iAccountService.Get10StudentNew();
            return Ok(list);
        }

        [HttpGet("show_tutor_have_ads")]
        public async Task<IActionResult> ShowTutorHaveAds()
        {
            var result = await _tutorService.GetAccountHaveAd();
            return Ok(result);
        }

        [HttpPut("create_request_withdraw_money")]
        public async Task<IActionResult> CreateRequestWithdrawMoney(RequestWithdrawMoneyVM model)
        {
            var userId = _currentUserService.GetUserId().ToString();
            Account user = await _userManager.FindByIdAsync(userId);
            var modelSigin = new UserSignIn() { UserName = user.UserName, Password = model.Password };
            var userAuhten = await iAccountService.SignInAsync(modelSigin);

            if(userAuhten == null)
            {
                return BadRequest("Invalid!!!");
            }
            float result = await iAccountService.CraeteRequestPaymentTransaction(userId, model.Amount, model.Type);
            if(result == 0) 
            {
                return BadRequest("Fail Request");
            }
            if(result == 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Invalid balance");
            }
        }

        [HttpGet("tutorid_by_accountid")]
        public async Task<IActionResult> GetTutorIdByAccountId(string accountId)
        {
            string id = await iAccountService.GetTutorIdByAccountId(accountId);
            return Ok(id);
        }
    }
}
