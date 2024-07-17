using BusinessObjects.Models;
using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using API.Services;
using Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

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
        [HttpPost("student_signup")]
        public async Task<IActionResult> StudentSignUp(StudentDTO signUpModel)
        {
            var result = await _accountService.StudentSignUpAsync(signUpModel);
            if (result > 0) return Ok();
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("tutor_signup")]
        public async Task<IActionResult> TutorSignUp(TutorDTO signUpModel)
        {
            var result = await _accountService.TutorSignUpAsync(signUpModel);
            if (result != null) return Ok(result);
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("signup")]
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
        [HttpPost("moderator_signup")]
        public async Task<IActionResult> SignUpModerator(SignUpModerator signUpModer)
        {
            var userIdSignUpId = await _accountService.SignUpModerator(signUpModer);
            if (userIdSignUpId == null)
            {
                return BadRequest("Email is existed");
            }
            if (userIdSignUpId != null)
            {
                var scheme = HttpContext.Request.Scheme;
                var host = HttpContext.Request.Host.Value;
                var url = $"{scheme}://{host}/api/auth/confirm-email?email={signUpModer.Email}";
                await _mailService.SendEmailAsync(signUpModer.Email, "Xác thực tài khoản của bạn", url);
                return Ok(new { userId = userIdSignUpId });
            }

            return StatusCode(500);
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(UserSignIn signIn)
        {
            var user = await _accountService.SignInAsync(signIn);
            if (user == null || !(user.IsActive))
            {
                return Unauthorized();
            }
            else if (!user.EmailConfirmed)
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

        [AllowAnonymous]
        [HttpPost("signin_google")]
        public async Task<IActionResult> SignInWithGoogle([FromBody] string gmail)
        {
            var user = await _accountService.SignInWithGG(gmail);
            if (user == null || !(user.IsActive))
            {
                return BadRequest("Account does not exist");
            }
            else if (!user.EmailConfirmed)
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

        [HttpDelete("signout")]
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
        [HttpPost("refresh_token")]
        public async Task<IActionResult> refeshToken(RefreshTokenVM refreshToken)
        {
            var user = await _accountService.GetAccountById(refreshToken.userId);
            if (user == null || !(user.IsActive) || user.RefreshToken != refreshToken.refreshToken || user.DateExpireRefreshToken < DateTime.UtcNow)
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
        [HttpGet("confirm_email")]
        public async Task<IActionResult> ConfirmAccount(string email)
        {
            var result = await _accountService.ConfirmAccount(email);
            if (result) return Ok(result);
            else return BadRequest("Cannot confirm your email");
        }

        [HttpPost("forget_password")]
        public async Task<IActionResult> ForgetPassword([FromBody] string email)
        {
            var token = await _accountService.TokenForgetPassword(email);
            if (token == null)
            {
                return BadRequest("Not Found!!!");
            }
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(token);
            var encodedToken = Base64UrlTextEncoder.Encode(plainTextBytes);
            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host.Value;
            var url = $"{scheme}://{host}/api/auth/redirectoResetPasswordPage?email={email}&token={encodedToken}";
            await _mailService.SendToken(email, "Token For ResetPassword", url);
            return Ok("Send email to confirm reset password");
        }

        [HttpGet("redirec_resetpasswordpage")]
        public async Task<IActionResult> RedirectoResetPasswordPage(string email, string token)
        {
            var result = _accountService.GetAccountByEmail(email);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(token);
        }

        [HttpPost("reset_password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var result = await _accountService.ResetPasswordEmail(model);
            return Ok(result);
        }
    }
}
