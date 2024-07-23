using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly WalletDAO walletDAO = null;
        private readonly DAOs.DbContext _dbContext;

        public WalletRepository(DAOs.DbContext dbContext)
        {
            if (walletDAO == null)
            {
                walletDAO = new WalletDAO();
            }
            _dbContext = dbContext;
        }

        public bool AddWallet(Wallet wallet)
        {
            return walletDAO.AddWallet(wallet);
        }

        public bool DelWallets(int id)
        {
            return walletDAO.DelWallets(id);
        }

        public List<Wallet> GetWallets()
        {
            return walletDAO.GetWallets();
        }

        public bool UpdateWallets(Wallet wallet)
        {
            return walletDAO.UpdateWallets(wallet);
        }

        public async Task<float?> UpdateBalance(string userId, float plusMoney)
        {
            var wallet = _dbContext.Wallets.FirstOrDefault(_ => _.AccountId == userId);
            wallet.Balance += plusMoney;
            _dbContext.Update(wallet);
            _dbContext.SaveChanges();
            var result = await _dbContext.Wallets.FirstOrDefaultAsync(_ => _.AccountId == userId);
            return result.Balance;
        }

        public async Task<float?> WithdrawMoney(string userId, float money)
        {
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(_ => _.AccountId == userId);
            if (wallet == null) return null;

            if (money > wallet.Balance) return null;

            wallet.Balance -= money;
            _dbContext.Update(wallet);
            _dbContext.SaveChanges();
            return wallet.Balance;
        }


    }
}
