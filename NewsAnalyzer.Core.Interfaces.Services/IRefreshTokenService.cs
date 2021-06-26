using NewsAnalyzer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Core.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<RefreshTokenDto> GenerateRefreshToken(Guid userId);
        Task<bool> CheckRefreshTokenIsValid(string refreshToken);
    }
}
