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
using Entities.DTO_s.Administrations;
using Microsoft.AspNetCore.Identity;

namespace Bussiness.Concrete
{
    public class AdminManager : IAdminServices
    {
        private IPostRepository _postRepository;
        private readonly UserManager<User> _userManager;
        private ICommentRepository _commentRepository;
        private IUserRepository _userRepository;
        private ILikeRepository _likeRepository;
        public AdminManager(UserManager<User> userManager)
        {
            _postRepository = new PostRepository();
            _commentRepository = new CommentRepository();
            _userManager = userManager;
            _userRepository = new UserRepository();
            _likeRepository = new LikeRepository();
        }

        public async Task<ICollection<User>> GetAllUsers()
        {

        var test = await _userRepository.GetAllUsers();
            return await _userRepository.GetAllUsers();
        }


        public async Task GiveRoleToUser(string userId, string Role = "User")
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not exist!");
            var userRoles = await _userManager.GetRolesAsync(user);
            if(userRoles.Count == 0)
                await _userManager.AddToRoleAsync(user, "USER");
            else if(userRoles.First() == Role)
                throw new Exception("User already has that role!");
            else
            {
                var removeResult = await _userManager.RemoveFromRoleAsync(user, userRoles.First().ToUpper());
                if (removeResult.Succeeded)
                {
                    var addResult = await _userManager.AddToRoleAsync(user, Role.ToUpper());
                    if (!addResult.Succeeded)
                        throw new Exception("Cant add role to the current user!!");
                }
                else
                    throw new Exception("Cant remove old role from the current user!!");
            }

        }


        public async Task SuspendUser(string userId, bool suspend = false)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user.AccountSuspended != suspend)
            {
                user.AccountSuspended = suspend;
                if (user == null)
                    throw new Exception("User not exist!");
                await _userRepository.UpdateUser(user);
            }
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

        public async Task<AllStaticsViewModel> GetAllStatistics(DateTime startDate, DateTime endDate)
        {
            return new AllStaticsViewModel
            {
                PostStatics = await _postRepository.GetAllPostStatistics( startDate,  endDate),
                CommentStatics = await _commentRepository.GetAllCommentStatistics( startDate,  endDate),
                LikeStatics = await _likeRepository.GetAllLikeStatistics( startDate,  endDate),
                UserCount = await _userRepository.UserCount()
            };

        }

    }
}
