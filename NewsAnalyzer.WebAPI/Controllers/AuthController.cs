using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAnalizer.Core.DataTransferObjects;
using NewsAnalizer.Core.Services.Interfaces;
using NewsAnalyzer.Util.Text;
using NewsAnalyzer.WebAPI.Auth;
using NewsAnalyzer.WebAPI.Controllers.Requests;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsAnalyzer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthController(IUserService userService, IRoleService roleService, IJwtAuthManager jwtAuthManager, IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _roleService = roleService;
            _jwtAuthManager = jwtAuthManager;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Rgister([FromBody] RegisterRequest request)
        {
            //ToDo validate request
            try
            {
                JwtAuthResult jwtAuthResult = null;
                if ((await _userService.GetByEmail(request.Email)) == null)
                {
                    var passwHash = Text.EncryptSHA256(request.Password);
                    var defaultRole = (await _roleService.GetRoles())
                        .FirstOrDefault(r => r.Name.Equals("User"));
                    var userDto = new UserDto()
                    {
                        Id = Guid.NewGuid(),
                        Email = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        PasswordHash = passwHash,
                        RoleId = defaultRole.Id,
                        RoleName = defaultRole.Name
                    };

                    var userRegistered = await _userService.Register(userDto);

                    if (userRegistered)
                    {
                        jwtAuthResult = await _jwtAuthManager.GenerateToken(userDto);
                    }
                    else
                    {
                        return BadRequest("User with such email already exists.");
                    }
                }
                return Ok(jwtAuthResult);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return StatusCode(500, "Unsuccessful registration.");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.GetByEmail(request.Email);
            if (user != null)
            {
                var pwdHash = Text.EncryptSHA256(request.Password);
                if (pwdHash.Equals(user.PasswordHash))
                {
                    var jwtAuthResult = await _jwtAuthManager.GenerateToken(user);
                    return Ok(jwtAuthResult);
                }
            }

            return BadRequest("Username or password incorrect.");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            if (!await _refreshTokenService.CheckRefreshTokenIsValid(request.Token))
            {
                return BadRequest("Refresh token is invalid.");
            }

            var user = await _userService.GetByRefreshToken(request.Token);
            if (user != null) {

                var jwtAuthResult = await _jwtAuthManager.GenerateToken(user);
                return Ok(jwtAuthResult); 
            }

            return BadRequest("Refresh token is incorrect.");
        }
    }
}
