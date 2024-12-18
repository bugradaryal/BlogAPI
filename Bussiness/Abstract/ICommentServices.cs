using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTO_s;

namespace Bussiness.Abstract
{
    public interface ICommentServices
    {
        Task AddCommentToPost(CommentViewModel commentViewModel);
        Task DeleteCommentFromPost(int commentId);
    }
}
