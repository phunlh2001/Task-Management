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
        Task<AppUser> GetByAccessTokenAsync(string accessToken);
        Task<bool> ValidateLogin(AppUser user, string password);
        Task<bool> RegistNewUser(AppUser user, string password, IEnumerable<string> roles);
        Task<bool> IsValidRefreshToken(string refreshToken, AppUser user);
        Task<bool> IsValidAccessToken(string accessToken);
        Task<bool> SetRefreshTokenAsync(AppUser user, string token);
        Task<string?> GetRefreshTokenAsync(AppUser user);
        Task<bool> RemoveTokenAsync(AppUser user);
        Task<TokenModel> GenerateTokensAsync(AppUser user);
        DateTime? GetExpiryDate(string accessToken);
        Task<string> GetNewAccessTokenAsync(AppUser user);
    }
}