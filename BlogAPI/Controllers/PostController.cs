using Bussiness.Abstract;
using Bussiness.Concrete;
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
        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts(int CurrentPage = 1)
        {
            try
            {
                var posts = await _postServices.GetAllPosts(CurrentPage);
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
    }
}
