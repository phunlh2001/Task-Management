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
                return await _urepo.GenerateAccessTokenAsync(user);
            }
            return null;
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

        public async Task<TokenModel> RefreshTokenAsync(TokenModel model, string username){
            if(model != null && await _urepo.IsValidToRefreshTokenAsync(model.AccessToken, model.RefreshToken)){
                var user = await _urepo.GetByUserNameAsync(username);
                await _urepo.RemoveTokenAsync(user);
                return await _urepo.GenerateAccessTokenAsync(user);
            }
            return null;
        }


    }
}