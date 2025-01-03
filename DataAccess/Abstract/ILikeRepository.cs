using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.DTO_s;

namespace DataAccess.Abstract
{
    public interface ILikeRepository
    {
        Task<ICollection<Like>> GetAllLikesByPostId(int postId);
        Task LikeThePost(Like like);
        Task DislikeThePost(Like like);
        Task<Like> GetLikeByValues(int postId, string userId);
        Task<bool> IsPostLiked(int postId, string userId);
        Task<ICollection<LikeStaticsViewModel>> GetAllLikeStatistics();
    }
}
