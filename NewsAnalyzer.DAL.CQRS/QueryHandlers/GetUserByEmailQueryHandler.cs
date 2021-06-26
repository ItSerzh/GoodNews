using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;
using NewsAnalyzer.DAL.CQRS.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.QueryHandlers
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly NewsAnalyzerContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserByEmailQueryHandler(NewsAnalyzerContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.Equals(request.Email), cancellationToken);
            
            return _mapper.Map<UserDto>(user);

        }
    }
}
