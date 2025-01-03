using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.DTO_s;

namespace DataAccess.Abstract
{
    public interface IPostRepository
    {
        Task AddPost(Post post);
        Task DeletePost(Post post);
        Task UpdatePost(Post post);
        Task<ICollection<Post>> GetAllPostsByIndex(int CurrentPage, int index);
        Task<Post> GetPostById(int postId);
        Task<int> PostCounts();
        Task<ICollection<PostStaticsViewModel>> GetAllPostStatistics();

    }
}
