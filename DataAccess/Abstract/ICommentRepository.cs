using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess.Abstract
{
    public interface ICommentRepository
    {
        Task AddCommentToPost(Comment comment);
        Task DeleteCommentFromPost(int commentId);
        Task<ICollection<Comment>> GetAllCommentsByPostId(int postId);
    }
}
