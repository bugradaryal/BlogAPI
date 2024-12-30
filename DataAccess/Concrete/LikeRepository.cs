﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class LikeRepository : ILikeRepository
    {
        public async Task<ICollection<Like>> GetAllLikesByPostId(int postId)
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Likes.Where(x => x.post_id == postId).ToListAsync();
            }
        }
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
    }
}
