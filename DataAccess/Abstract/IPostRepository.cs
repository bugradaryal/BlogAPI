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
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post> GetPostById(int postId);
    }
}
