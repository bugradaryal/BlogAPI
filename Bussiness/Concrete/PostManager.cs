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
        private ILikeRepository _likeRepository;
        private ICommentRepository _commentRepository;
        public PostManager() 
        {
            _postRepository = new PostRepository();
            _likeRepository = new LikeRepository();
            _commentRepository = new CommentRepository();
        }


        public async Task<ICollection<Post>> GetAllPostsByIndex(int CurrentPage, int index)
        {
            return await _postRepository.GetAllPostsByIndex(CurrentPage, index);
        }

        public async Task<int> PostCounts()
        {
            var posts = await _postRepository.PostCounts();
            return posts;
        }

        public async Task<Post> GetPostById(int postId)
        {
            return await _postRepository.GetPostById(postId);
        }

        public async Task LikeThePost(int postId, string userId)
        {
            var test = await _likeRepository.IsPostLiked(postId, userId);
            if (await _likeRepository.IsPostLiked(postId, userId))
                throw new Exception("Post already liked!");
            await _likeRepository.LikeThePost(new Like
            {
                post_id = postId,
                user_id = userId,
            });
        }
        public async Task DislikeThePost(int postId, string userId)
        {
            if (!await _likeRepository.IsPostLiked(postId, userId))
                throw new Exception("Post already dissliked!");
            var like = await _likeRepository.GetLikeByValues(postId, userId);

            await _likeRepository.DislikeThePost(like);
        }
        public async Task<ICollection<Post>> GetPostBySearch(string title, int index)
        {
            return await _postRepository.GetPostBySearch(title, index);
        }
    }
}
