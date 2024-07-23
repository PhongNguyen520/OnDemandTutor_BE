using AutoMapper;
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
        private IMapper _mapper;

        public WalletRepository(DAOs.DbContext dbContext, IMapper mapper)
        {
            if (walletDAO == null)
            {
                walletDAO = new WalletDAO();
            }
            _dbContext = dbContext;
            _mapper = mapper;
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

        public async Task<List<PaymentTransactionVM>> GetRequestWithdraw()
        {
            var list = await _dbContext.PaymentTransactions.Include(_ => _.Wallet)
                                       .Where(_ => _.IsValid == null).ToListAsync();
            var result = _mapper.Map<List<PaymentTransactionVM>>(list);
            return result;
        }

        public async Task<bool> ChangeStatusWallet(string id, bool status, float amount)
        {
            var wal =await _dbContext.PaymentTransactions.FirstOrDefaultAsync(_ => _.Id == id);
            if (wal != null)
            {
                wal.IsValid = status;

                _dbContext.Update(wal);
                _dbContext.SaveChanges();

                if (wal.IsValid == true)
                {
                    var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(_ => _.WalletId == wal.WalletId);
                    wallet.Balance += (amount);
                    _dbContext.Update(wallet);
                    _dbContext.SaveChanges();
                }
                return true;
            }
            return false;
        } 
    }
}
