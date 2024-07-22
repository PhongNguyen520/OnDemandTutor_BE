using API.Services;
using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace API.Controllers
{
    [Route("api/wallet")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IClassService _classService;
        private readonly UserManager<Account> _userManager;
        private readonly IAccountService _accountService;
        private readonly DAOs.DbContext _dbContext;

        public WalletController(ICurrentUserService currentUserService, IClassService classService, IAccountService accountService, UserManager<Account> userManager, DAOs.DbContext dbContext)
        {
            _walletService = new WalletService();
            _currentUserService = currentUserService;
            _classService = classService;
            _accountService = accountService;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("viewlist")]

        public IActionResult GetList()
        {
            return Ok(_walletService.GetWallets());
        }


        [HttpPost]
        [Route("create_wallet")]

        public IActionResult Create([FromBody] CreateWallet _wallet)
        {
            var wallet = new Wallet()
            {
                WalletId = Guid.NewGuid().ToString(),
                CreateDay = DateTime.Now,
                Balance = 0,
                AccountId = _wallet.Id,
            };

            return Ok(_walletService.AddWallet(wallet));
        }

        [HttpGet]
        [Route("get_wallet")]
        public IActionResult Get()
        {
            var userId = _currentUserService.GetUserId().ToString();
            var response = _walletService.GetWallets().FirstOrDefault(w => w.AccountId == userId);
            return Ok(response);
        }

        [HttpPut]
        [Route("update_wallet")]
        public IActionResult UpdateWallet(string bankname, string banknumber)
        {
            var userId = _currentUserService.GetUserId().ToString();
            var response = _walletService.GetWallets().FirstOrDefault(w => w.AccountId == userId);
            response.BankName = bankname;
            response.BankNumber = banknumber;
            _walletService.UpdateWallets(response);
            return Ok(response);
        }
        [HttpPost("reload_balance")]
        public async Task<IActionResult> ReloadBalance()
        {
            var userId = _currentUserService.GetUserId().ToString();
            var newBa = await _classService.PaymentTutor(userId);
            if (newBa == null) 
            {
                return BadRequest("Not Balance!");
            }
            var result = await _walletService.UpdateBalance(userId, newBa.PlusMoney);
            
            return Ok(result);
        }

        [HttpPost("withdraw_money")]
        public async Task<IActionResult> WithdrawMoney([FromBody] string password, float withdrawMoney)
        {
            var userId = _currentUserService.GetUserId().ToString();
            var result = await _userManager.FindByIdAsync(userId);
            var check = await _userManager.CheckPasswordAsync(result, password);
            if (!check) return BadRequest();

            var wit = _walletService.WithdrawMoney(userId, withdrawMoney);
           
            return Ok(wit);
        }
    }
}
