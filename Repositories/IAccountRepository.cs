using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAccountRepository
    {
        public bool AddAccount(Account account);

        public bool DelAccounts(int id);

        public List<Account> GetAccounts();
        
        public bool UpdateAccounts(Account account);
        Task<Account> GetAccountById(string id);
        Task<String> GetAccountByEmail(string email);
        Task<String> SignUpAsync(AccountDTO model);
        Task<string> TutorSignUpAsync(TutorDTO model);
        Task<int> StudentSignUpAsync(StudentDTO model);
        Task<Account> SignInAsync(UserSignIn model);
        Task<IList<String>> GetRolesAsync(Account user);
        Task<bool> ConfirmAccount(string email);
        Task<int> EnalbleUser(String userId);
        Task<String> SignUpModerator(SignUpModerator model);
        Task<Account> SignInWithGG(string gmail);
        Task<string> TokenForgetPassword(string email);
        Task<int> ResetPasswordEmail(ResetPasswordModel model);
        Task<IQueryable<TutorInterVM>> GetAccountTutorIsActiveFalse();
        Task<bool> CheckAccountByEmail(string email);
        Task<IQueryable<UserRolesVM>> GetAllIsActive();
    }
}
