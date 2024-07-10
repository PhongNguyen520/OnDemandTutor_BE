using AutoMapper;
using BusinessObjects;
using BusinessObjects.Constrant;
using BusinessObjects.Models;
using DAOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO accountDAO = null;
        private UserManager<Account> _userManager;
        private SignInManager<Account> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private IMapper _mapper;
        private readonly DAOs.DbContext _dbContext;

        public AccountRepository()
        {
            if (accountDAO == null)
            {
                accountDAO = new AccountDAO();
            }
        }

        public AccountRepository(UserManager<Account> userManager,
            SignInManager<Account> signInManager, IConfiguration configuration,
            RoleManager<IdentityRole> roleManager, IMapper mapper, DAOs.DbContext dbContext)
        {
            
            if (accountDAO == null)
            {
                accountDAO = new AccountDAO();
            }
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public AccountRepository(UserManager<Account> userManager,
            SignInManager<Account> signInManager, IConfiguration configuration,
            RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            if (accountDAO == null)
            {
                accountDAO = new AccountDAO();
            }
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public bool AddAccount(Account account)
        {
            return accountDAO.AddAccount(account);
        }

        public bool DelAccounts(int id)
        {
            return accountDAO.DelAccounts(id);
        }

        public List<Account> GetAccounts()
        {
            return accountDAO.GetAccounts();
        }

        public bool UpdateAccounts(Account account)
        {
            return accountDAO.UpdateAccounts(account);
        }

        public async Task<IList<string>> GetRolesAsync(Account user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<Account> SignInAsync(UserSignIn model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                return user;
            }
            return null;
        }

        public async Task<Account> SignInWithGG(string gmail)
        {
            var result = await _userManager.FindByEmailAsync(gmail);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<String> SignUpModerator(SignUpModerator model)
        {
            var isDupplicate = await _userManager.FindByEmailAsync(model.Email);
            if (isDupplicate != null)
            {
                return null;
            }
            var user = _mapper.Map<Account>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, AppRole.Moderator);
                return user.Id;
            }
            return null;
        }

            public async Task<String> SignUpAsync(AccountDTO model)
        {
            var isDupplicate = await _userManager.FindByEmailAsync(model.Email);
            if (isDupplicate != null)
            {
                return null;
            }
            var user = _mapper.Map<Account>(model);

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // init role in the system
                if (!await _roleManager.RoleExistsAsync(AppRole.Tutor))
                {
                    await _roleManager.CreateAsync(new IdentityRole(AppRole.Tutor));
                }

                if (!await _roleManager.RoleExistsAsync(AppRole.Student))
                {
                    await _roleManager.CreateAsync(new IdentityRole(AppRole.Student));
                }

                if (!await _roleManager.RoleExistsAsync(AppRole.SystemHandler))
                {
                    await _roleManager.CreateAsync(new IdentityRole(AppRole.SystemHandler));
                }

                if (!await _roleManager.RoleExistsAsync(AppRole.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(AppRole.Admin));
                }

                if (!await _roleManager.RoleExistsAsync(AppRole.Moderator))
                {
                    await _roleManager.CreateAsync(new IdentityRole(AppRole.Moderator));
                }

                // role init is student
                if (model.isAdmin)
                {
                    await _userManager.AddToRoleAsync(user, AppRole.Tutor);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, AppRole.Student);
                }

            }
            return user.Id;

        }

        public async Task<IQueryable<UserRolesVM>> GetAll()
        {
            var listUserRolesVM = new List<UserRolesVM>();
            var listUser = accountDAO.GetAccounts().ToList();
            foreach (var user in listUser.ToList())
            {
                var userRoles = (await GetRolesAsync(user));
                if (userRoles.Contains(AppRole.Admin))
                {
                    listUser.Remove(user);
                }
                else
                {
                    var userRolesVM = _mapper.Map<UserRolesVM>(user);
                    userRolesVM.RolesName = userRoles.ToList();
                    listUserRolesVM.Add(userRolesVM);
                }
            }
            return listUserRolesVM.AsQueryable();
        }

        public async Task<Account> GetAccountById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<String> GetAccountByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user.Email;
        }

        public async Task<bool> ConfirmAccount(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;
            user.EmailConfirmed = true;
            var result = accountDAO.UpdateAccounts(user);
            return result;
        }

        public async Task<int> EnalbleUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsActive = !(user.IsActive);
                _dbContext.Update(user);
                return await _dbContext.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<string> TutorSignUpAsync(TutorDTO model)
        {
            var userId = Guid.NewGuid().ToString();
            var tutor = _mapper.Map<Tutor>(model);
            tutor.TutorId = userId; 
            _dbContext.Add(tutor);
            _dbContext.SaveChanges();
            return userId;
        }

        public async Task<int> StudentSignUpAsync(StudentDTO model)
        {
            var userId = Guid.NewGuid().ToString();
            var student = _mapper.Map<Student>(model);
            student.StudentId = userId;
            _dbContext.Add(student);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<string> TokenForgetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }


        public async Task<int> ResetPasswordEmail(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return 0;   
            }

            var base64EncodedBytes = System.Convert.FromBase64String(model.Token);
            var tokenEnCode =  System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            var result = await _userManager.ResetPasswordAsync(user, tokenEnCode, model.Password);
            if (result.Succeeded)
            {
                return 1;
            }
            return 2;
        }

        public async Task<IQueryable<TutorInterVM>> GetAccountTutorIsActiveFalse()
        {
            var listTutor = _dbContext.Tutors.Where(t => t.IsActive == false)
                                      .Include(a => a.Account)
                                      .Where(a => a.Account.IsActive == true)
                                      .ToList();
            var listTutorVM = new List<TutorInterVM>();
            foreach(var tutor in listTutor)
            {
                var tutorVM = _mapper.Map<TutorInterVM>(tutor);
                listTutorVM.Add(tutorVM);
            }

            return listTutorVM.AsQueryable();

        }

        public async Task<bool> CheckAccountByEmail(string email)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                return true;
            }
            return false;
        }

        public async Task<IQueryable<UserRolesVM>> GetAllIsActive()
        {
            var listUserRolesVM = new List<UserRolesVM>();
            var listUser = accountDAO.GetAccounts().ToList();
            foreach (var user in listUser.ToList())
            {
                var userRoles = (await GetRolesAsync(user));
                if (userRoles.Contains(AppRole.Admin) || user.IsActive == false)
                {
                    listUser.Remove(user);
                }
                else
                {
                    var userRolesVM = _mapper.Map<UserRolesVM>(user);
                    userRolesVM.RolesName = userRoles.ToList();
                    listUserRolesVM.Add(userRolesVM);
                }
            }
            return listUserRolesVM.AsQueryable();
        }

    }
}
