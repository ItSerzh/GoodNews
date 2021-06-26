using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAnalizer.Core.Services.Interfaces;
using NewsAnalyzer.Util.Text;
using NewsAnalyzer.WebAPI.Auth;
using NewsAnalyzer.WebAPI.Controllers.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsAnalyzer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtAuthManager _jwtAuthManager;

        public TokenController(IUserService userService, IJwtAuthManager jwtAuthManager)
        {
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody]LoginRequest request)
        {
            JwtAuthResult jwtAuthResult = null;
            var user = await _userService.GetByEmail(request.Email);
            if (user != null)
            {
                var pwdHash = Text.EncryptSHA256(request.Password);
                if (pwdHash.Equals(user.PasswordHash))
                {
                    var claims = new[]
                    {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.RoleName)
                    };
                    
                    jwtAuthResult = await _jwtAuthManager.GenerateToken(user);
                }
            }
            return Ok(jwtAuthResult);
        }

    }
}
