using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Dtos.Authentication;
using backend.Models.Entities;

namespace backend.Services.Interfaces
{
    public interface IAuthenticationService
    {
        string ExtractAccessTokenFromRequest(HttpRequest context);
        Task<bool> IsDuplicateUserName(string username);
        Task<AppUser> GetUserAsync(string username);
        Task<TokenModel> LoginAsync(LoginModel model);
        Task<bool> ValidateRefreshToken(string refreshToken, string accessToken);
        Task<bool> ValidateAccessToken(string token);
        Task<bool> RegisterMemberAsync(RegisterModel model);
        Task<bool> RegisterAdminAsync(RegisterModel model);
        Task<bool> RevokeTokenAsync(string username);
        Task<TokenModel> RefreshTokenAsync(string accessToken, string refreshToken);
    }
}