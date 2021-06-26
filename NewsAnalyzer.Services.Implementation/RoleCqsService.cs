using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.Core.Services.Interfaces;
using NewsAnalyzer.Dal.Repositories.Implementation;
using NewsAnalyzer.Dal.Repositories.Interfaces;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;
using NewsAnalyzer.DAL.CQRS.Queries;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Services.Implementation
{
    public class RoleCqlService : IRoleService
    {
        private IMediator _mediator;
        private IMapper _mapper;

        public RoleCqlService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<bool> AddRoleToUser(string userName, RoleDto role)
        {
            /*var user = await _unitOfWork.User
                .FindBy(u => u.Email.Equals(userName))
                .FirstOrDefaultAsync();

            if (user != null && _unitOfWork.Role.GetById(role.Id) != null)
            {
                user.RoleId = role.Id;
                await _unitOfWork.User.Update(user);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0;
            }*/
            return false;
        }

        public async Task<IEnumerable<RoleDto>> GetRoles()
        {
            var getRolesQuery = new GetRolesQuery();
            return await _mediator.Send(getRolesQuery);
        }

        public async Task<RoleDto> GetUserRole(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
