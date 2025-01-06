using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities;
using Entities.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class LikeRepository : ILikeRepository
    {

        public async Task LikeThePost(Like like)
        {
            using (var _DBContext = new DataDbContext())
            {
                await _DBContext.Likes.AddAsync(like);
                await _DBContext.SaveChangesAsync();
            }
        }

        public async Task DislikeThePost(Like like)
        {
            using (var _DBContext = new DataDbContext())
            {
                _DBContext.Likes.Remove(like);
                await _DBContext.SaveChangesAsync();
            }
        }
        public async Task<Like> GetLikeByValues(int postId, string userId)
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Likes.Where(x => x.post_id == postId).FirstAsync(y => y.user_id == userId);
            }
        }

        public async Task<bool> IsPostLiked(int postId, string userId)
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Likes.Where(x => x.post_id == postId).AnyAsync(y => y.user_id == userId);
            }
        }

        public async Task<ICollection<LikeStaticsViewModel>> GetAllLikeStatistics(DateTime endDate, DateTime startDate)
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Likes.Where(like => like.Date >= startDate && like.Date < endDate)
            .GroupBy(like => new { like.Date.Year, like.Date.Month })
            .Select(group => new LikeStaticsViewModel
            {
                Year = group.Key.Year,
                Month = group.Key.Month,
                LikeCount = group.Count()
            })
            .OrderBy(result => result.Year)
            .ThenBy(result => result.Month)
            .ToListAsync();
            }
        }
    }
}
