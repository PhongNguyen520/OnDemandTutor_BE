using BusinessObjects.Models;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using API.Services;
using Repositories;
using Microsoft.Extensions.Hosting;

namespace API.Controller
{
    [Route("api/auth")]
    [ApiController]

    public class AuthenticateController : ControllerBase
    {
        private IMailService _mailService;
        private IAccountService _accountService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticateController(IJwtTokenService jwtTokenService, IAccountService accountService, 
            ICurrentUserService currentUserService, IMailService mailService)
        {
            _mailService = mailService;
            _accountService = accountService;
            _jwtTokenService = jwtTokenService;
            _currentUserService = currentUserService;
        }

        [AllowAnonymous]
        [HttpPost("student-signUp")]
        public async Task<IActionResult> StudentSignUp(StudentDTO signUpModel)
        {
            var result = await _accountService.StudentSignUpAsync(signUpModel);
            if (result > 0) return Ok();
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("tutor-signUp")]
        public async Task<IActionResult> TutorSignUp(TutorDTO signUpModel)
        {
            var result = await _accountService.TutorSignUpAsync(signUpModel);
            if (result > 0) return Ok();
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp(AccountDTO signUpModel)
        {
            var userIdSignUpId = await _accountService.SignUpAsync(signUpModel);
            if (userIdSignUpId == null)
            {
                return BadRequest("Email is existed");
            }
            if (userIdSignUpId != null)
            {
                var scheme = HttpContext.Request.Scheme;
                var host = HttpContext.Request.Host.Value;
                var url = $"{scheme}://{host}/api/auth/confirm-email?email={signUpModel.Email}";
                await _mailService.SendEmailAsync(signUpModel.Email, "Xác thực tài khoản của bạn", url);
                return Ok(new { userId = userIdSignUpId });
            }

            return StatusCode(500);
        }



        [AllowAnonymous]
        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(UserSignIn signIn)
        {
            var user = await _accountService.SignInAsync(signIn);
            if (user == null || !(user.IsActive))
            {
                return Unauthorized();
            } else if (!user.EmailConfirmed)
            {
                return BadRequest("Cần phải xác thực tài khoản của bạn qua email đăng kí");
            }
            var userRoles = await _accountService.GetRolesAsync(user);
            var accessToken = _jwtTokenService.CreateToken(user, userRoles);
            var refreshToken = _jwtTokenService.CreateRefeshToken();
            user.RefreshToken = refreshToken;
            user.DateExpireRefreshToken = DateTime.Now.AddDays(7);
            var result = _accountService.UpdateAccounts(user);
            if (result)
            {
                return Ok(new { token = accessToken, refreshToken });

            }
            return BadRequest("Failed to update user's token");
        }

        [HttpDelete("signOut")]
        public async Task<IActionResult> SignOut()
        {
            var user = await _currentUserService.GetUser();
            if (user is null)
                return Unauthorized();
            user.RefreshToken = null;
            _accountService.UpdateAccounts(user);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> refeshToken([FromBody] string refreshToken)
        {
            var userId = _currentUserService.GetUserId();
            var user = await _accountService.GetAccountById(userId.ToString());
            if (user == null || !(user.IsActive) || user.RefreshToken != refreshToken || user.DateExpireRefreshToken < DateTime.UtcNow)
            {
                return BadRequest("Not permission");
            }
            var userRoles = await _accountService.GetRolesAsync(user);
            var newRefreshToken = _jwtTokenService.CreateRefeshToken();
            user.RefreshToken = newRefreshToken;
            user.DateExpireRefreshToken = DateTime.Now.AddDays(7);
            var token = _jwtTokenService.CreateToken(user, userRoles);
            _accountService.UpdateAccounts(user);
            return Ok(new { token = token, refreshToken = newRefreshToken });
        }

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmAccount(string email)
        {
            var result = await _accountService.ConfirmAccount(email);
            if (result) return Ok(result);
            else return BadRequest("Cannot confirm your email");
        }

    }
}
