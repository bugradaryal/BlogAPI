using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess.Abstract
{
    public interface ILikeRepository
    {
        Task<ICollection<Like>> GetAllLikesByPostId(int postId);
        Task LikeThePost(Like like);
        Task DislikeThePost(Like like);
        Task<Like> GetLikeById(int likeId);
        Task<bool> IsPostLiked(int postId, string userId);
    }
}
