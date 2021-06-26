using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewsAnalizer.Core.DataTransferObjects;
using NewsAnalizer.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.WebAPI.Auth
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private IConfiguration _configuration;
        private readonly IRefreshTokenService _refreshTokenService;

        public JwtAuthManager(IConfiguration configuration, IRefreshTokenService refreshTokenService = null)
        {
            _configuration = configuration;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<JwtAuthResult> GenerateToken(UserDto user)
        {
            var jwtToken = new JwtSecurityToken("NewsAnalyzer",
                "NewsAnalyzer",
                GenerateClaims(user),
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        _configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = await _refreshTokenService.GenerateRefreshToken(user.Id);

            return new JwtAuthResult()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        private Claim[] GenerateClaims(UserDto user)
        {
            return new[]
            {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.RoleName)
            };
        }
    }

    public class JwtAuthResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
