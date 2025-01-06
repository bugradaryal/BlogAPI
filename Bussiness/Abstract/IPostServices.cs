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
        Task<ICollection<Post>> GetAllPostsByIndex(int CurrentPage, int index);
        Task<int> PostCounts();
        Task LikeThePost(int postId, string userId);
        Task DislikeThePost(int postId, string userId);
        Task<ICollection<Post>> GetPostBySearch(string title, int index);
    }
}
