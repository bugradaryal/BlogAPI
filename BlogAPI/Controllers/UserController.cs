using Bussiness.Abstract;
using Bussiness.Concrete;
using Entities;
using Entities.DTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserServices _userServices;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager) 
        {
            _userServices = new UserManager(userManager, signInManager);
        }

        [Authorize]
        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount([FromBody] string user_id)
        {
            try
            {
                await _userServices.DeleteUser(user_id);
                return Ok(new { message = "Account successfully deleted!" });
            }
            catch (Exception ex) 
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount(UserViewModel userViewModel)
        {
            try
            {
                await _userServices.UpdateUser(userViewModel);
                return Ok(new { message = "Account successfully updated!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordViewModel passwordViewModel)
        {
            try
            {
                await _userServices.ChangePassword(passwordViewModel.User_Id, passwordViewModel.NewPassword, passwordViewModel.OldPassword);
                return Ok(new { message = "Password successfully changed!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
