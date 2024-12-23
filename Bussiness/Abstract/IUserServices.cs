using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Bussiness.Abstract
{
    public interface IUserServices
    {
        Task CreateUser(User user, string password);
        Task DeleteUser(User user);
        Task UpdateUser(User user);
        Task<User> LoginUser(string email, string password);
        Task<User> GetUserByEmail(string email);
    }
}
