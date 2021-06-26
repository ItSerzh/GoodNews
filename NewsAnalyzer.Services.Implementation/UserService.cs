using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.Core.Services.Interfaces;
using NewsAnalyzer.Dal.Repositories.Implementation;
using NewsAnalyzer.Dal.Repositories.Interfaces;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Services.Implementation
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Register(UserDto model)
        {
            try
            {
                var defaultRole = await _unitOfWork.Role
                    .FindBy(r => r.Name.Equals("User"))
                    . FirstOrDefaultAsync();
                model.RoleId = defaultRole.Id;
                _unitOfWork.User.Add(_mapper.Map<User>(model));
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Error has occured durin registration user: {model.Email} ");
                return false;
            }
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            var user = await _unitOfWork.User
                .FindBy(u => u.Email.Equals(email))
                .FirstOrDefaultAsync();

            return _mapper.Map<UserDto>(user);
        }

        public Task<UserDto> GetByRefreshToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
