using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Data.Context;
using backend.Defaults;
using backend.Models.Dtos.Authentication;
using backend.Models.Entities;
using backend.Security.JWT;
using Microsoft.AspNetCore.Identity;

namespace backend.Data.Repositories
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JWTTokenProvider _jwtProvider;
        private readonly IConfiguration _configuration;
        public UserRepository(TaskManagerContext db,
        UserManager<AppUser> userManager,
        JWTTokenProvider jwtProvider,
        IConfiguration configuration) : base(db)
        {
            _userManager = userManager;
            _jwtProvider = jwtProvider;
            _configuration = configuration;
        }

        public async Task<AppUser> GetByUserNameAsync(string username){
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || user.IsDeleted) return null;
            return user;
        }

        public async override Task<AppUser> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null || user.IsDeleted) return null;
            return user;
        }

        public async Task<bool> ValidateLogin(AppUser user, string password){
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> RegistNewUser(AppUser user, string password, IEnumerable<string> roles){
            try{
                List<IdentityResult> results = new()
                {
                    await _userManager.CreateAsync(user, password),
                    await _userManager.AddToRolesAsync(user, roles)
                };
                foreach (var result in results){
                    string msg = "";
                    if(!result.Succeeded){
                        msg += string.Join("\n", result.Errors.Select(e=>e.Description));
                    }
                    if(!string.IsNullOrWhiteSpace(msg))
                        throw new Exception(msg);
                }
            }catch(Exception e){
                Console.WriteLine("Error: "+e.Message);
                return false;
            }
            return true;
        }

        


        public async Task<bool> SetTokenAsync(AppUser user, string token){
            var rs  = await _userManager.SetAuthenticationTokenAsync(user, AppTokenProvider.Name, TokenType.RefreshToken, token);
            if(!rs.Succeeded) return false;
            return true;
        }

        public async Task<string?> GetTokenAsync(AppUser user){
            return await _userManager.GetAuthenticationTokenAsync(user, AppTokenProvider.Name, TokenType.RefreshToken);
        }

        public async Task<bool> RemoveTokenAsync(AppUser user){
            var rs = await _userManager.RemoveAuthenticationTokenAsync(user, AppTokenProvider.Name, TokenType.RefreshToken);
            return rs.Succeeded;
        }

        

        public async Task<bool> IsValidToRefreshTokenAsync(string accesToken, string refreshToken){
            if(string.IsNullOrEmpty(accesToken)) return false;
            var principal = _jwtProvider.GetPrincipalOfIssuedToken(accesToken);
            if(principal == null) return false;
            Console.WriteLine(principal.Identity.Name);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if(user == null || await GetTokenAsync(user) != refreshToken 
                || _jwtProvider.GetExpiryDate(accesToken) <= DateTime.Now)
                return false;

            return true;
        }

        public async Task<TokenModel> GenerateAccessTokenAsync(AppUser user){
            
            if(user != null){
                var userRoles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "Manager")
                };
                foreach (var userRole in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var accessToken = _jwtProvider.GenerateAccessToken(claims);
                var refreshToken = _jwtProvider.GenerateRefreshToken();
                await SetTokenAsync(user, refreshToken);
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                return new TokenModel{
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresIn = DateTime.Now.AddDays(refreshTokenValidityInDays)
                };
            }
            return null;
        }
    }
}