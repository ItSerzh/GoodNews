using MediatR;
using NewsAnalyzer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.Commands
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public Guid RoleId { get; set; }
    }
}
