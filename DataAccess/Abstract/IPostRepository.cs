using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess.Abstract
{
    public interface IPostRepository
    {
        Task AddPost(Post post);
        Task DeletePost(Post post);
        Task UpdatePost(Post post);
        Task<ICollection<Post>> GetAllPosts(int CurrentPage);
        Task<Post> GetPostById(int postId);
        Task<int> PostCounts();
        Task<ICollection<Post>> GetAllPostForModerator(int CurrentPage);

    }
}
