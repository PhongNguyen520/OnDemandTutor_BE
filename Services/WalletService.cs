using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository iWalletRepository;

        //public WalletService()
        //{
        //    if (iWalletRepository == null)
        //    {
        //        iWalletRepository = new WalletRepository();
        //    }
        //}
        public WalletService(IWalletRepository _iWalletRepository)
        {
            this.iWalletRepository = _iWalletRepository;
        }

        public bool AddWallet(Wallet wallet)
        {
            return iWalletRepository.AddWallet(wallet);
        }

        public bool DelWallets(int id)
        {
            return iWalletRepository.DelWallets(id);
        }

        public List<Wallet> GetWallets()
        {
            return iWalletRepository.GetWallets();
        }

        public bool UpdateWallets(Wallet wallet)
        {
            return iWalletRepository.UpdateWallets(wallet);
        }

        public async Task<float?> UpdateBalance(string userId, float plusMoney)
        {
            return await iWalletRepository.UpdateBalance(userId, plusMoney);
        }

        public async Task<float?> WithdrawMoney(string userId, float money)
        {
            return await iWalletRepository.WithdrawMoney(userId, money);
        }

        public async Task<List<PaymentTransactionVM>> GetRequestWithdraw()
        {
            return await iWalletRepository.GetRequestWithdraw();
        }

        public async Task<bool> ChangeStatusWallet(string id, bool status, float amount)
        {
            return await iWalletRepository.ChangeStatusWallet(id, status, amount);
        }
    }
}

