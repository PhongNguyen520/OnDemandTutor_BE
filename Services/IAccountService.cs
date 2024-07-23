using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Identity;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountService
    {
        public bool AddAccount(Account account);

        public bool DelAccounts(int id);

        public List<Account> GetAccounts();

        public Task<Account> GetAccountById(string id);

        Task<String> GetAccountByEmail(string email);
        public bool UpdateAccounts(Account account);
        Task<string> TutorSignUpAsync(TutorDTO model);
        Task<int> StudentSignUpAsync(StudentDTO model);
        Task<String> SignUpAsync(AccountDTO model);
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
        Task<IQueryable<UserRolesVM>> ListAccountIsActive();
        Task<List<Student10VM>> Get10StudentNew();
        Task<List<TutorInterVM>> Get10TutorNew();
        Task<float> CraeteRequestPaymentTransaction(string userId, float amount, int type);
    }
}
