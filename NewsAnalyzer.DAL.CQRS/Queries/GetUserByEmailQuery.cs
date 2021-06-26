using MediatR;
using NewsAnalyzer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.Queries
{
    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        public string Email { get; set; }
    }
}
