using NewsAnalyzer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Register(UserDto model);
        Task<UserDto> GetByEmail(string email);
        Task<UserDto> GetByRefreshToken(string token);
    }
}
