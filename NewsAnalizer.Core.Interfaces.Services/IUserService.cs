using NewsAnalizer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalizer.Core.Services.Interfaces
{
    public interface IUserService
    {
        string GetPasswordHash(string password);
        Task<bool> Register(UserDto model);
        Task<UserDto> GetByEmail(string email);
    }
}
