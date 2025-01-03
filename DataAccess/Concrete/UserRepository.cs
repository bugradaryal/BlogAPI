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
        public async Task<ICollection<User>> GetAllUsers(){
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Users.ToListAsync();
            }
        }
        public async Task UpdateUser(User user)
        {
            using (var _DBContext = new DataDbContext())
            {
                _DBContext.Users.Update(user);
                await _DBContext.SaveChangesAsync();
            }
        }

        public async Task<bool> AnyUser(string userId)
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Users.AnyAsync(x=>x.Id == userId);
            }
        }

        public async Task<int> UserCount()
        {
            using (var _DBContext = new DataDbContext())
            {
                return await _DBContext.Users.CountAsync();
            }
        }
    }
}
