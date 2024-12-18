using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    {
        public async Task UpdateUser(User user)
        {
            using (var _DBContext = new DataDbContext())
            {
                _DBContext.Users.Update(user);
                await _DBContext.SaveChangesAsync();
            }
        }
    }
}
