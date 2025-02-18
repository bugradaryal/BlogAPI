﻿using Azure.Core;
using Bussiness.Abstract;
using Bussiness.Concrete;
using Entities;
using Entities.DTO_s;
using Entities.DTO_s.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace stajAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserServices _userServices;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private ITokenServices _tokenServices;
        private IEmailServices _emailServices;
        private IOptions<JWT> _jwt;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JWT> jwt, IOptions<EmailSender> emailsender) 
        {
            _userServices = new UserManager(userManager, signInManager);
            _tokenServices = new TokenManager(jwt, userManager);
            _emailServices = new EmailManager(emailsender, userManager);
            _jwt = jwt;
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = ModelState });
                 await _userServices.CreateUser(new User { Name = registerViewModel.Name, Surname = registerViewModel.Surname, UserName = registerViewModel.UserName, Email = registerViewModel.Email, Address = registerViewModel.Address, PhoneNumber = registerViewModel.PhoneNumber },registerViewModel.Password);

                return Ok(new { message = "Register is succesfull!!"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = ModelState });
                var user = await _userServices.LoginUser(loginViewModel.Email, loginViewModel.Password);
                var token = _tokenServices.CreateTokenJWT(user);
                var refleshtoken = _tokenServices.GenerateRefreshToken();
                await _tokenServices.SaveRefreshTokenAsync(user,refleshtoken);
                Response.Headers.Append("Authorization", "Bearer " + token);
                Response.Headers.Append("RefreshToken", refleshtoken);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("ValidateToken")]
        [Authorize]
        public async Task<IActionResult> ValidateToken()
        {
            try
            {
                var result = await _tokenServices.ValidateToken(this.HttpContext);
                if (result.user.AccountSuspended)
                {
                    Response.Headers.Remove("Authorization");
                    Response.Headers.Remove("RefreshToken");
                    return BadRequest(new { message = "User is suspended!!" });
                }
                if (!await _userServices.AnyUser(result.user.Id))
                {
                    Response.Headers.Remove("Authorization");
                    Response.Headers.Remove("RefreshToken");
                    return BadRequest(new { message = "User is not exist!!" });
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message });
            }
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var refleshToken = Request.Headers["RefreshToken"].FirstOrDefault();
                if (refleshToken == null)
                    return BadRequest(new { message = "RefreshToken not found." });
                var user = await _tokenServices.GetUserFromRefreshToken(refleshToken);
                if (user == null)
                    return BadRequest(new { message = "User not found." });
                var newRefreshToken = _tokenServices.GenerateRefreshToken();
                await _tokenServices.SaveRefreshTokenAsync(user, newRefreshToken);
                Response.Headers.Append("Authorization", "Bearer " + _tokenServices.CreateTokenJWT(user));
                Response.Headers.Append("RefreshToken", newRefreshToken);

                return Ok(new { message = "Login Succesfull With RefreshToken" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("SendMail")]
        [AllowAnonymous]
        public async Task<IActionResult> SendingEmail([FromBody]string email)
        {
            try
            {
                var user = await _userServices.GetUserByEmail(email);
                if (user == null)
                    return BadRequest(new { message = "Account does not exist!" });

                var emailConfUrl = await _tokenServices.CreateTokenEmailConfirm(user);
                var callback_url = "http://localhost:3000/EmailVerification?userId=" + user.Id + "&emailConfUrl=" + emailConfUrl;


                _emailServices.SendingEmail(email, callback_url);
                return Ok(new { message = "Email verification code sended!!!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("Emailverification")]
        [AllowAnonymous]
        public async Task<IActionResult> Emailverification([FromBody] EmailVeificationViewModel emailVeificationViewModel)
        {
            try
            {
                await _emailServices.ConfirmEmail(emailVeificationViewModel.userId, emailVeificationViewModel.emailConfUrl);
                return Ok(new { message = "Your email has been successfully confirmed!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
