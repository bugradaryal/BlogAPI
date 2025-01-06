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
                return await _DBContext.Posts.OrderBy(x => x.id).Skip((CurrentPage - 1) * index).Take(index)
                    .Include(y => y.comments).Include(z => z.likes).Include(c => c.categories).ToListAsync();
            }
        }

        public async Task<Post> GetPostById(int postId)
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Posts.Include(x => x.comments).Include(y => y.likes).Include(z => z.categories).FirstAsync(c => c.id == postId);
            }
        }

        public async Task<ICollection<PostStaticsViewModel>> GetAllPostStatistics(DateTime startDate, DateTime endDate)
        {
            using (var _DBContext = new DataDbContext())
            {
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

        public async Task<ICollection<Post>> GetPostBySearch(string title, int index)
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Posts
                    .Where(x => x.Title.ToLower().Contains(title.ToLower()))  // Başlıkla arama
                    .Include(y => y.comments)  // İlgili yorumları dahil et
                    .Include(z => z.likes)  // İlgili beğenileri dahil et
                    .Include(c => c.categories)  // İlgili kategorileri dahil et
                    .Skip((index - 1) * 4)  // Sayfa numarasına göre atla
                    .Take(4)  // Sayfa başına kaç veri alınacağını belirle
                    .ToListAsync();
            }
        }
    }
}
