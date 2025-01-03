using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess.Abstract
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAllUsers();
        Task UpdateUser(User user);
        Task<bool> AnyUser(string userId);
        Task<int> UserCount();
    }
}
