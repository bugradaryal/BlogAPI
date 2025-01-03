using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities;
using Entities.DTO_s;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class PostRepository : IPostRepository
    {
        public async Task AddPost(Post post)
        {
            using (var _DBContext = new DataDbContext())
            {
                await _DBContext.Posts.AddAsync(post);
                await _DBContext.SaveChangesAsync();
            }
        }


        public async Task DeletePost(Post post)
        {
            using (var _DBContext = new DataDbContext())
            {
                _DBContext.Posts.Remove(post);
                await _DBContext.SaveChangesAsync();
            }
        }


        public async Task UpdatePost(Post post)
        {
            using (var _DBContext = new DataDbContext())
            {
                _DBContext.Posts.Update(post);
                await _DBContext.SaveChangesAsync();
            }
        }

        public async Task<int> PostCounts()
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Posts.CountAsync();
            }
        }

        public async Task<ICollection<Post>> GetAllPostsByIndex(int CurrentPage, int index)
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Posts.OrderBy(x => x.id).Skip((CurrentPage - 1) * index).Take(index).ToListAsync();
            }
        }

        public async Task<Post> GetPostById(int postId)
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Posts.FirstAsync(x => x.id == postId);
            }
        }

        public async Task<ICollection<PostStaticsViewModel>> GetAllPostStatistics()
        {
            using (var _DBContext = new DataDbContext())
            {
                var endDate = DateTime.UtcNow;
                var startDate = endDate.AddYears(-1);
                return await _DBContext.Posts.Where(post => post.Date >= startDate && post.Date < endDate)
            .GroupBy(post => new { post.Date.Year, post.Date.Month })
            .Select(group => new PostStaticsViewModel
            {
                Year = group.Key.Year,
                Month = group.Key.Month,
                PostCount = group.Count()
            })
            .OrderBy(result => result.Year)
            .ThenBy(result => result.Month)
            .ToListAsync();
            }
        }
    }
}
