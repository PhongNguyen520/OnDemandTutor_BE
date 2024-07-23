using BusinessObjects;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IWalletService
    {
        public bool AddWallet(Wallet wallet);

        public bool DelWallets(int id);

        public List<Wallet> GetWallets();

        public bool UpdateWallets(Wallet wallet);

        Task<float?> UpdateBalance(string userId, float plusMoney);

        Task<float?> WithdrawMoney(string userId, float money);

        Task<List<PaymentTransactionVM>> GetRequestWithdraw();

        Task<bool> ChangeStatusWallet(string id, bool status, float amount);

        Task<bool> Create2RefundPaymentTransaction(string StudentId, float money);
    }
}
