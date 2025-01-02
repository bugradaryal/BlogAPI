using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.DTO_s;

namespace Bussiness.Abstract
{
    public interface IUserServices
    {
        Task CreateUser(User user, string password);
        Task DeleteUser(string user_id);
        Task UpdateUser(UserViewModel userViewModel);
        Task<User> LoginUser(string email, string password);
        Task<User> GetUserByEmail(string email);
        Task ChangePassword(string user_id, string newpassword, string oldpassword);
    }
}
