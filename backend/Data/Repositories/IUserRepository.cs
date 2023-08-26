using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Dtos.Authentication;
using backend.Models.Entities;

namespace backend.Data.Repositories
{
    public interface IUserRepository: IRepository<AppUser>
    {
        Task<AppUser> GetByUserNameAsync(string username);
        Task<bool> ValidateLogin(AppUser user, string password);
        Task<bool> RegistNewUser(AppUser user, string password, IEnumerable<string> roles);
        Task<bool> IsValidToRefreshTokenAsync(string accesToken, string refreshToken);
        Task<bool> SetTokenAsync(AppUser user, string token);
        Task<string?> GetTokenAsync(AppUser user);
        Task<bool> RemoveTokenAsync(AppUser user);
        Task<TokenModel> GenerateAccessTokenAsync(AppUser user);
    }
}