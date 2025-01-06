using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public class CommentRepository : ICommentRepository
    {
        public async Task AddCommentToPost(Comment comment)
        {
            using (var _DBContext = new DataDbContext())
            {
                await _DBContext.Comments.AddAsync(comment);
                await _DBContext.SaveChangesAsync();
            }
        }

        public async Task DeleteCommentFromPost(int commentId)
        {
            using (var _DBContext = new DataDbContext())
            {
                var comment = await _DBContext.Comments.FirstOrDefaultAsync(x => x.id == commentId);
                _DBContext.Comments.Remove(comment);
                await _DBContext.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Comment>> GetAllCommentsByPostId(int postId)
        {
            using (var _DBContext = new DataDbContext())
            {
                var comment = await _DBContext.Comments.Where(x => x.post_id == postId).ToListAsync();
                return comment;
            }
        }

        public async Task<ICollection<CommentStaticsViewModel>> GetAllCommentStatistics(DateTime startDate, DateTime endDate)
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Comments.Where(comment => comment.Date >= startDate && comment.Date < endDate)
            .GroupBy(comment => new { comment.Date.Year, comment.Date.Month })
            .Select(group => new CommentStaticsViewModel
            {
                Year = group.Key.Year,
                Month = group.Key.Month,
                CommentCount = group.Count()
            })
            .OrderBy(result => result.Year)
            .ThenBy(result => result.Month)
            .ToListAsync();
            }
        }

    }
}
