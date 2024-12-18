using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities;
using Entities.DTO_s;

namespace Bussiness.Concrete
{
    public class CommentManager : ICommentServices
    {
        private ICommentRepository _commentRepository;
        public CommentManager() 
        {
            _commentRepository = new CommentRepository();
        }

        public async Task AddCommentToPost(CommentViewModel commentViewModel)
        {
            await _commentRepository.AddCommentToPost(new Comment
            {
                user_id = commentViewModel.user_id,
                post_id = commentViewModel.post_id,
                Content = commentViewModel.Content,
            });
        }

        public async Task DeleteCommentFromPost(int commentId)
        {
            await _commentRepository.DeleteCommentFromPost(commentId);
        }
    }
}
