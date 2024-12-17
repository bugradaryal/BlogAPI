using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities;
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


        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Posts.ToListAsync();
            }
        }

        public async Task<Post> GetPostById(int postId)
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Posts.Where(x => x.id == postId).FirstAsync();
            }
        }

    }
}
