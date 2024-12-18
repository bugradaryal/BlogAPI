using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Bussiness.Abstract
{
    public interface IPostServices
    {
        Task<Post> GetPostById(int postId);
        Task<IEnumerable<Post>> GetAllPosts(int CurrentPage);
        Task<int> PostCounts();
        Task LikeThePost(int postId, string userId);
        Task DislikeThePost(int likeId);
    }
}
