using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using backend.Data.Repositories;
using backend.Models.Dtos;
using backend.Models.Dtos.Authentication;
using backend.Models.Entities;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _userService;

        public AuthenticationController(IAuthenticationService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Validation username
        /// </summary>
        /// <param name="userName">The username</param>
        /// <response code="200">Valid username</response>
        /// <response code="400">Username already exists</response>
        /// <returns>JSON contains data</returns>
        [HttpGet("isValidUserName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IsValidUserName([FromQuery]string userName)
        {
            if (await _userService.IsDuplicateUserName(userName)) return BadRequest();
            return Ok();
        }

        /// <summary>
        /// Login to page
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///
        ///         {
        ///           "Username": "anqh123",
        ///           "Password": "@1234"
        ///         }
        ///         
        /// </remarks>
        /// <response code="200">Get the token</response>
        /// <response code="400">Invalid content-type</response>
        /// <response code="404">Username or password not found</response>
        [HttpPost("login")]
        [ProducesDefaultResponseType(typeof(Response<TokenModel>))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {

            if(!ModelState.IsValid) return BadRequest();
            var tokens = await _userService.LoginAsync(model);
            if(tokens == null) return NotFound();
        
            return Ok(new Response<TokenModel>{
                Message = "Token content:",
                Data = tokens,
                StatusCode = HttpStatusCode.OK
            });
        }

        /// <summary>
        /// Register the username and password
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///
        ///         {
        ///            "UserName": "phuaa123",
        ///            "FullName": "Nguyen Van A",
        ///            "Password": "12345@",
        ///            "ConfirmPassword": "12345@",
        ///            "Email": "user@example.com"
        ///         }
        ///         
        /// </remarks>
        /// <response code="200">Register success</response>
        /// <response code="400">Invalid content-type</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterModel model)
        {
            if(!ModelState.IsValid) return BadRequest();
            if(await _userService.RegisterMemberAsync(model) == false){
                return BadRequest();
            }
        
            return Ok(new Response<string>{
                Message = "Registed success!",
                StatusCode = HttpStatusCode.OK
            });
        }

        [HttpPut("refreshToken")]
        [ProducesResponseType(typeof(Response<TokenModel>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody]string refreshToken)
        {
            var accessToken = _userService.ExtractAccessTokenFromRequest(Request);
            Console.WriteLine("token lengh is: "+accessToken.Length);
            if(!await _userService.ValidateAccessToken(accessToken)
            || !await _userService.ValidateRefreshToken(refreshToken, accessToken)
            ) return BadRequest();
            var tokens = await _userService.RefreshTokenAsync(accessToken, refreshToken);

            if(tokens == null){
                return BadRequest(new Response<string>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Cannot create refresh token."
                });
            }
        
            return Ok(new Response<TokenModel>{
                Message = "Refreshed token!",
                StatusCode = HttpStatusCode.OK,
                Data = tokens
            });
        }
        
        [HttpDelete("revokeToken")]
        [Authorize]
        [ProducesResponseType(typeof(Response<string>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RevokeToken()
        {
            if(!await _userService.RevokeTokenAsync(User.Identity.Name)){
                return BadRequest();
            }
        
            return Ok(new Response<string>{
                Message = "Revoked token!",
                StatusCode = HttpStatusCode.OK
            });
        }

        [HttpGet("test")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> TestAccess()
        {
            

            return Ok(new Response<string>
            {
                Message = "Access!",
                StatusCode = HttpStatusCode.OK
            });
        }
        
        [Authorize]
        [HttpGet("whoAmI")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> WhoAmI()
        {
            
            return Ok(new Response<string>
            {
                Message = "User name is:",
                StatusCode = HttpStatusCode.OK,
                Data = User.Identity.Name
            });
        }

    }
}