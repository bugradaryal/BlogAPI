using Bussiness.Abstract;
using Bussiness.Concrete;
using Entities;
using Entities.DTO_s;
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
        private ICommentServices _commentServices;
        private ICategoryServices _categoryServices;
        public AdminController(UserManager<User> userManager) 
        {
            _adminServices = new AdminManager(userManager);
            _postServices = new PostManager();
            _commentServices = new CommentManager();
            _categoryServices = new CategoryManager();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _adminServices.GetAllUsers());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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
        public async Task<IActionResult> AddPost([FromBody]AddPostViewModel postViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = ModelState });
                var categoryId = await _categoryServices.GetCategoryIdByName(postViewModel.Category);
                await _adminServices.AddPost(new Post
                {
                    Title = postViewModel.Title,
                    Content = postViewModel.Content,
                    Image = postViewModel.Image,
                    category_id = categoryId,
                });
                return Ok(new {message = "Post added!"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("RemovePost")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RemovePost([FromBody] RemovePostViewModel removePostViewModel)
        {
            try
            {
                var post = await _postServices.GetPostById(removePostViewModel.postId);
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

        [HttpPut("UpdatePost")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdatePost(UpdatePostViewModel postViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = ModelState });
                var post = await _postServices.GetPostById(postViewModel.Id);
                var categoryId = await _categoryServices.GetCategoryIdByName(postViewModel.Category);
                post.Title = postViewModel.Title;
                post.Content = postViewModel.Content;
                post.Image = postViewModel.Image;
                post.Date = DateTime.Now;
                post.category_id = categoryId;

                await _adminServices.UpdatePost(post);
                return Ok("Post updated!!");
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteCommentFromPost")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCommentFromPost(int commentId)
        {
            try
            {
                await _commentServices.DeleteCommentFromPost(commentId);
                return Ok("Post deleted!!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost("GetAllStatistics")]
        public async Task<IActionResult> GetAllStatistics([FromBody] DateViewModel dateViewModel)
        {
            try
            {
                return Ok(await _adminServices.GetAllStatistics(dateViewModel.startdate, dateViewModel.enddate));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
