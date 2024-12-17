using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Bussiness.Abstract
{
    public interface IAdminServices
    {
        Task AddPost(Post post);
        Task DeletePost(Post post);
        Task UpdatePost(Post post);
    }
}
