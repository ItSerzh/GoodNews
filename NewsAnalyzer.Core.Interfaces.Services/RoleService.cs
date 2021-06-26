using NewsAnalyzer.Core.DataTransferObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Core.Services.Interfaces
{
    public interface IRoleService
    {
        Task<RoleDto> GetUserRole(string userName);
        Task<bool> AddRoleToUser(string userName, RoleDto role);
        Task<IEnumerable<RoleDto>> GetRoles();
    }
}
