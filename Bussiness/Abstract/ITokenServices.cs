using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.DTO_s;
using Microsoft.AspNetCore.Http;

namespace Bussiness.Abstract
{
    public interface ITokenServices
    {
        Task<User> GetUserFromRefreshToken(string refreshToken);
        Task SaveRefreshTokenAsync(User user, string refreshToken);
        string GenerateRefreshToken();
        Task<TokenValidationViewModel> ValidateToken(HttpContext context);
        string CreateTokenJWT(User user);
    }
}
