using Entities.DTO_s;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bussiness.Abstract;
using Bussiness.Concrete;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentServices _commentServices; 
        public CommentController() 
        {
            _commentServices = new CommentManager();
        }

        [HttpPost("AddCommentToPost")]
        [Authorize]
        public async Task<IActionResult> AddCommentToPost(CommentViewModel commentViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = ModelState });
                await _commentServices.AddCommentToPost(commentViewModel);
                return Ok("Comment added to the post!!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
