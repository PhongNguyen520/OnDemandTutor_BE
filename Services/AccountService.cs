using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository iAccountRepository;



        public AccountService(IAccountRepository _iAccountRepository)
        {
            iAccountRepository = _iAccountRepository;
        }

        public bool AddAccount(Account account)
        {
            return iAccountRepository.AddAccount(account);
        }

        public bool DelAccounts(int id)
        {
            return iAccountRepository.DelAccounts(id);
        }

        public List<Account> GetAccounts()
        {
            return iAccountRepository.GetAccounts();
        }

        public async Task<Account> GetAccountById(string id)
        {
            return await iAccountRepository.GetAccountById(id);
        }

        public Task<IList<string>> GetRolesAsync(Account user)
        {
            return iAccountRepository.GetRolesAsync(user);
        }

        public Task<Account> SignInAsync(UserSignIn model)
        {
            return iAccountRepository.SignInAsync(model);
        }

        public async Task<String> SignUpAsync(AccountDTO model)
        {
            return await iAccountRepository.SignUpAsync(model);
        }

        public bool UpdateAccounts(Account account)
        {
            return iAccountRepository.UpdateAccounts(account);
        }

        public Task<bool> ConfirmAccount(string email)
        {
            return iAccountRepository.ConfirmAccount(email);    
        }

        public async Task<int> EnalbleUser(string userId)
        {
            return await iAccountRepository.EnalbleUser(userId);
        }

        public async Task<int> TutorSignUpAsync(TutorDTO model)
        {
            return await iAccountRepository.TutorSignUpAsync(model);
        }

        public async Task<String> GetAccountByEmail(string email)
        {
            return await iAccountRepository.GetAccountByEmail(email);
        }
        public async Task<int> StudentSignUpAsync(StudentDTO model)
        {
            return await iAccountRepository.StudentSignUpAsync(model);
        }
        public async Task<String> SignUpModerator(SignUpModerator model)
        {
            return await iAccountRepository.SignUpModerator(model);
        }

        public Task<Account> SignInWithGG(string gmail)
        {
            return iAccountRepository.SignInWithGG(gmail);
        }

        public async Task<string> TokenForgetPassword(string email)
        {
            return await iAccountRepository.TokenForgetPassword(email);
        }

        public async Task<int> ResetPasswordEmail(ResetPasswordModel model)
        {
            return await iAccountRepository.ResetPasswordEmail(model);
        }

        public Task<IQueryable<TutorInterVM>> GetAccountTutorIsActiveFalse()
        {
            return iAccountRepository.GetAccountTutorIsActiveFalse();
        }

        public Task<bool> CheckAccountByEmail(string email)
        {
            return iAccountRepository.CheckAccountByEmail(email);
        }
    }
}
