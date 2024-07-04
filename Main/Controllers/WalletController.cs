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
        private readonly IWalletService walletService;

        public WalletController()
        {
            walletService = new WalletService();
        }

        [HttpGet]
        [Route("viewlist")]

        public IActionResult GetList()
        {
            return Ok(walletService.GetWallets());
        }


        [HttpPost]
        [Route("create_wallet/{id}")]

        public IActionResult Create(string id, [FromBody] CreateWallet request)
        {
            var wallet = new Wallet()
            {
                WalletId = Guid.NewGuid().ToString(),
                Balance = request.Balance,
                BankName = request.BankName,
                BankNumber = request.BankNumber,
                CreateDay = request.CreateDay,
                AccountId = id,
            };


            return Ok(walletService.AddWallet(wallet));
        }

        [HttpPost]
        [Route("getwallet/{id}")]
        public IActionResult Get(string id)
        {
            var response = walletService.GetWallets().FirstOrDefault(w => w.AccountId == id);
            return Ok(response);
        }

        //[HttpPut]
        //[Route("update_amount/{id}")]

        //public IActionResult Update(string id)
        //{
        //    var response = walletService.GetWallets().FirstOrDefault(w => w.AccountId == id);
        //}
    }
}
