using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models.Dtos.Authentication;

namespace backend.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<TokenModel> LoginAsync(LoginModel model);
        Task<bool> RegisterMemberAsync(RegisterModel model);
        Task<bool> RegisterAdminAsync(RegisterModel model);
        Task<bool> RevokeTokenAsync(string username);
        Task<TokenModel> RefreshTokenAsync(TokenModel model, string username);
    }
}