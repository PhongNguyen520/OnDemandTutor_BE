using API.Services;
using Azure;
using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;

namespace API.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class PaymentTransactionController : ControllerBase
    {
        private readonly IPaymentTransactionService _transactionService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IWalletService _walletService;
        private readonly IPaymentService _paymentService;

        public PaymentTransactionController(ICurrentUserService currentUserService)
        {
            _transactionService = new PaymentTransactionService();
            _currentUserService = currentUserService;
            _walletService = new WalletService();
            _paymentService = new PaymentService();
        }

        [HttpGet]
        [Route("view_transactionlist")]

        public IActionResult GetLists()
        {
            return Ok(_transactionService.GetTransactions());
        }


        [HttpGet]
        [Route("get_transaction")]
        public IActionResult GetTransaction()
        {
            var userId = _currentUserService.GetUserId().ToString();
            
            var wallet = _walletService.GetWallets().Where(s => s.AccountId == userId).First();

            var payments = _paymentService.GetPayments().Where(s => s.WalletId == wallet.WalletId);

            List<PaymentTransaction> result = new List<PaymentTransaction>();

            foreach (var payment in payments)
            {
                var transactions = _transactionService.GetTransactions().Where(s => s.PaymentId == payment.Id).ToList();
                foreach (var transaction in transactions)
                {
                    result.Add(transaction);
                }
            }

            return Ok(result);
        }
    }
}
