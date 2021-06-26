using NewsAnalyzer.Core.DataTransferObjects;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsAnalyzer.WebAPI.Auth
{
    public interface IJwtAuthManager
    {
        public Task<JwtAuthResult> GenerateToken(UserDto user);
    }
}