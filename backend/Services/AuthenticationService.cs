using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data.Repositories;
using backend.Defaults;
using backend.Models.Dtos.Authentication;
using backend.Models.Entities;
using backend.Security.JWT;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace backend.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly IUserRepository _urepo;
        private readonly IMapper _mapper;


        public AuthenticationService(IUserRepository urepo, IMapper mapper)
        {
            _urepo = urepo;
            _mapper = mapper;
        }

        public async Task<TokenModel> LoginAsync(LoginModel model)
        {
            var user = await _urepo.GetByUserNameAsync(model.UserName);
            if(await _urepo.ValidateLogin(user, model.Password)){
                return await _urepo.GenerateTokensAsync(user);
            }
            return null;
        }

        public async Task<bool> IsDuplicateUserName(string username)
        {
            if (await _urepo.GetByUserNameAsync(username) != null) return true;
            return false;
        }

        public async Task<bool> RegisterMemberAsync(RegisterModel model){
            var user = _mapper.Map<AppUser>(model);
            return await _urepo.RegistNewUser(user, model.Password, new[] { RoleName.Member });
        }

        public async Task<bool> RegisterAdminAsync(RegisterModel model){
            var user = _mapper.Map<AppUser>(model);
            return await _urepo.RegistNewUser(user, model.Password, RoleName.Roles);
        }

        public async Task<bool> RevokeTokenAsync(string username){
            var user = await _urepo.GetByUserNameAsync(username);
            if(user == null) return false;
            return await _urepo.RemoveTokenAsync(user);
        }

        public async Task<bool> ValidateRefreshToken(string refreshToken, string accessToken){
            var user = await _urepo.GetByAccessTokenAsync(accessToken);
            return await _urepo.IsValidRefreshToken(refreshToken, user);
        }

        public async Task<bool> ValidateAccessToken(string token){
            return await _urepo.IsValidAccessToken(token);
        }

        public async Task<TokenModel> RefreshTokenAsync(string accessToken, string refreshToken){
                var user = await _urepo.GetByAccessTokenAsync(accessToken);
                var newAccessToken = await _urepo.GetNewAccessTokenAsync(user);
                return new TokenModel{
                    AccessToken = newAccessToken,
                    RefreshToken = refreshToken,
                    ExpiresIn = (DateTime)_urepo.GetExpiryDate(newAccessToken)
                };
        }

        public string? ExtractAccessTokenFromRequest(HttpRequest context)
        {
            if(string.IsNullOrWhiteSpace(context.Headers.Authorization)) return null;
            return context.Headers.Authorization.ToString()["Bearer ".Length..];
        }

        public async Task<AppUser?> GetUserAsync(string username)
        {
            return await _urepo.GetByUserNameAsync(username);
        }
    }
}