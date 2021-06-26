using MediatR;
using NewsAnalizer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.Queries
{
    public class GetUserByRefreshTokenQuery : IRequest<UserDto>
    {
        public string RefreshToken { get; set; }
    }
}
