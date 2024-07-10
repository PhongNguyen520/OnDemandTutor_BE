using API.Services;
using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/wallet")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly ICurrentUserService _currentUserService;

        public WalletController(ICurrentUserService currentUserService)
        {
            _walletService = new WalletService();
            _currentUserService = currentUserService;
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
        [Route("getwallet")]
        public IActionResult Get()
        {
            var userId = _currentUserService.GetUserId().ToString();
            var response = _walletService.GetWallets().FirstOrDefault(w => w.AccountId == userId);
            return Ok(response);
        }

        [HttpPut]
        [Route("update_wallet")]

        public IActionResult Update(string BankName, string BankNumber)
        {
            var userId = _currentUserService.GetUserId().ToString();
            var wallet = _walletService.GetWallets().FirstOrDefault(w => w.AccountId == userId);
            wallet.BankName = BankName;
            wallet.BankNumber = BankNumber;
            return Ok(_walletService.UpdateWallets(wallet));
        }
    }
}
