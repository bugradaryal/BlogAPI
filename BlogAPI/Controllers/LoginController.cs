using Azure.Core;
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
    public class LoginController : ControllerBase
    {
        private IUserServices _userServices;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private ITokenServices _tokenServices;
        public LoginController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JWT> jwt) 
        {
            _userServices = new UserManager(userManager, signInManager);
            _tokenServices = new TokenManager(jwt, userManager);
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = ModelState });
                 await _userServices.CreateUser(new User { Name = registerViewModel.Name, Surname = registerViewModel.Surname, UserName = registerViewModel.UserName, Email = registerViewModel.Email, Adress = registerViewModel.Adress, },registerViewModel.Password);

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
        [AllowAnonymous]
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
    }
}
