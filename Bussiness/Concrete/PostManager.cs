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


        public async Task<IEnumerable<Post>> GetAllPosts(int CurrentPage)
        {
            var posts = await _postRepository.GetAllPosts(CurrentPage);
            foreach (var post in posts)
            {
                post.likes = await _likeRepository.GetAllLikesByPostId(post.id);
                post.comments = await _commentRepository.GetAllCommentsByPostId(post.id);
            }

            return posts;
        }

        public async Task<int> PostCounts()
        {
            var posts = await _postRepository.PostCounts();
            return posts;
        }

        public async Task<Post> GetPostById(int postId)
        {
            var post = await _postRepository.GetPostById(postId);
            post.likes = await _likeRepository.GetAllLikesByPostId(post.id);
            post.comments = await _commentRepository.GetAllCommentsByPostId(post.id);
            return post;
        }

        public async Task LikeThePost(int postId, string userId)
        {
            if (await _likeRepository.IsPostLiked(postId, userId))
                throw new Exception("Post already liked!");
            await _likeRepository.LikeThePost(new Like
            {
                post_id = postId,
                user_id = userId,
            });
        }
        public async Task DislikeThePost(int likeId)
        {
            var like = await _likeRepository.GetLikeById(likeId);
            await _likeRepository.DislikeThePost(like);
        }
    }
}
