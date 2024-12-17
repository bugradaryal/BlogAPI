using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussiness.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities;

namespace Bussiness.Concrete
{
    public class AdminManager : IAdminServices
    {
        private IPostRepository _postRepository;
        public AdminManager()
        {
            _postRepository = new PostRepository();
        }

        public async Task AddPost(Post post)
        {
            await _postRepository.AddPost(post);
        }

        public async Task DeletePost(Post post)
        {
            await _postRepository.DeletePost(post);
        }

        public async Task UpdatePost(Post post)
        {
            await _postRepository.UpdatePost(post);
        }

    }
}
