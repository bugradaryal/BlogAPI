using Bussiness.Abstract;
using Bussiness.Concrete;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace stajAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPostServices _postServices;
        public PostController() 
        {
            _postServices = new PostManager();
        }

        [AllowAnonymous]
        [HttpGet("GetAllPostsByIndex")]
        public async Task<IActionResult> GetAllPostsByIndex(int CurrentPage = 1, int index = 4)
        {
            try
            {
                var posts = await _postServices.GetAllPostsByIndex(CurrentPage, index);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("GetPostById")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            try
            {
                var post = await _postServices.GetPostById(postId);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("GetPostCounts")]
        public async Task<IActionResult> PostCounts()
        {
            try
            {
                var Count = await _postServices.PostCounts();
                return Ok(Count);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("LikeThePost")]
        public async Task<IActionResult> LikeThePost(int postId, string userId)
        {
            try
            {
                await _postServices.LikeThePost(postId, userId);
                return Ok("Post Liked!!");
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("DislikeThePost")]
        public async Task<IActionResult> DislikeThePost(int postId, string userId)
        {
            try
            {
                await _postServices.DislikeThePost(postId, userId);
                return Ok("Post Disliked!!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("GetPostBySearch")]
        public async Task<IActionResult> GetPostBySearch(string title)
        {
            try
            {
                var posts = await _postServices.GetPostBySearch(title);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
