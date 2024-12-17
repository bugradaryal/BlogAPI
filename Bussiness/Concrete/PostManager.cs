using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities;

namespace Bussiness.Concrete
{
    public class PostManager : IPostServices
    {
        private IPostRepository _postRepository;
        public PostManager() 
        {
            _postRepository = new PostRepository();
        }


        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            var posts = await _postRepository.GetAllPosts();
            return posts;
        }

        public async Task<Post> GetPostById(int postId)
        {
            var post = await _postRepository.GetPostById(postId);
            return post;
        }
    }
}
