using AutoMapper;
using MediatR;
using NewsAnalizer.Core.DataTransferObjects;
using NewsAnalizer.Core.Services.Interfaces;
using NewsAnalyzer.DAL.CQRS.Commands;
using NewsAnalyzer.DAL.CQRS.Queries;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalizer.Services.Implementation
{
    public class UserCqsService : IUserService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserCqsService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            try
            {
                var getUserByEmailQuery = new GetUserByEmailQuery() { Email = email };
                return await _mediator.Send(getUserByEmailQuery);
            }
            catch (Exception e)
            {
                Log.Error($"Error in GetByEmail method. { e.Message}");
                throw;
            }
        }

        public async Task<UserDto> GetByRefreshToken(string refreshToken)
        {
            try
            {
                var getUserEmailByRefreshTokenQuery = new GetUserByRefreshTokenQuery() { RefreshToken = refreshToken };
                return await _mediator.Send(getUserEmailByRefreshTokenQuery);
            }
            catch (Exception e)
            {
                Log.Error($"Error in GetByRefreshToken method. { e.Message}");
                throw;
            }
            
        }

        public async Task<bool> Register(UserDto model)
        {
            
            var registerUserCommand = new RegisterUserCommand()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = model.PasswordHash,
                RoleId = model.RoleId
            };
            return await _mediator.Send(registerUserCommand);
        }
    }
}
