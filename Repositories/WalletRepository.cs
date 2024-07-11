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

        public WalletRepository()
        {
            if (walletDAO == null)
            {
                walletDAO = new WalletDAO();
            }
        }

        public WalletRepository(DAOs.DbContext dbContext)
        {
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

        public async Task<float?> CreaterHistoryTransaction(HistoryTransaction transaction)
        {
            if (transaction == null)
            {
                return null;
            }
            _dbContext.Add(transaction);
            _dbContext.SaveChanges();
            return transaction.Amount;
        }

        public async Task<float?> UpdateBalance(string userId, float plusMoney)
        {
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(_ => _.AccountId == userId);
            wallet.Balance += plusMoney;
            _dbContext.Update(wallet);
            _dbContext.SaveChanges();
            return wallet.Balance;
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
