using NewsAnalizer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalizer.Core.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<RefreshTokenDto> GenerateRefreshToken(Guid userId);
        Task<bool> CheckRefreshTokenIsValid(string refreshToken);
    }
}
