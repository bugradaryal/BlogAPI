using Bussiness.Abstract;
using Bussiness.Concrete;
using Entities;
using Entities.DTO_s.Administrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace stajAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminServices _adminServices;
        private IPostServices _postServices;
        public AdminController(UserManager<User> userManager) 
        {
            _adminServices = new AdminManager(userManager);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("GiveRoleToUser")]
        public async Task<IActionResult> GiveRoleToUser(RoleViewModel roleViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = ModelState });
                await _adminServices.GiveRoleToUser(roleViewModel.userId, roleViewModel.Role);
                return Ok(new { message = "User has new role!" });
            }
            catch (Exception ex) 
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("SuspendUser")]
        public async Task<IActionResult> SuspendUser(string userId, bool suspend)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    return BadRequest(new { message = "İnvalid user id!!" });
                await _adminServices.SuspendUser(userId, suspend);
                return Ok(new { message = "Done!!" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("AddPost")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddPost(PostViewModel postViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = ModelState });
                await _adminServices.AddPost(new Post
                {
                    Title = postViewModel.Title,
                    Content = postViewModel.Content,
                    Image = postViewModel.Image
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("RemovePost")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RemovePost(int postId)
        {
            try
            {
                var post = await _postServices.GetPostById(postId);
                if (post == null)
                    return BadRequest(new { message = "Post not exist!" });
                await _adminServices.DeletePost(post);
                return Ok(new {message = "Post Deleted!"});
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("UpdatePost")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdatePost(PostViewModel postViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = ModelState });
                await _adminServices.UpdatePost(new Post
                {
                    id = postViewModel.Id,
                    Title = postViewModel.Title,
                    Content = postViewModel.Content,
                    Image = postViewModel.Image
                });
                return Ok("Post updated!!");
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
    }
}
