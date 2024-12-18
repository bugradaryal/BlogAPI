using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Bussiness.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace Bussiness.Concrete
{
    public class UserManager: IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private IUserRepository _userRepository;
        public UserManager(UserManager<User> userManager, SignInManager<User> signInManager) 
        {
            _userRepository = new UserRepository();
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task CreateUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user,password);
            if(result.Succeeded)
            {
                var rolresult = await _userManager.AddToRoleAsync(user,"User");
                if (!rolresult.Succeeded)
                    throw new Exception(message: rolresult.Errors.ToString());
            }
            else
                throw new Exception(message:result.Errors.ToString());
        }

        public async Task DeleteUser(User user)
        {
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new Exception(message: result.Errors.ToString());
        }

        public async Task UpdateUser(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception(message: result.Errors.ToString());
        }

        public async Task<User> LoginUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception(message:"User not exist!");
            if (user.AccountSuspended)
                throw new Exception(message: "User's account suspended!");
            var result = await _signInManager.PasswordSignInAsync(user,password, false, true);
            if (result.Succeeded)
                return user;
            else if(result.IsLockedOut)
                throw new Exception(message: "Too many attempt. Account is locked for 5 min!");
            throw new Exception(message: "Email or password is wrong!");
        }
    }
}
