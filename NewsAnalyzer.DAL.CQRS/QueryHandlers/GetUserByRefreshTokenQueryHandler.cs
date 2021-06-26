using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAnalizer.Core.DataTransferObjects;
using NewsAnalizer.DAL.Core;
using NewsAnalizer.DAL.Core.Entities;
using NewsAnalyzer.DAL.CQRS.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.QueryHandlers
{
    public class GetUserByRefreshTokenQueryHandler : IRequestHandler<GetUserByRefreshTokenQuery, UserDto>
    {
        private readonly NewsAnalizerContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserByRefreshTokenQueryHandler(NewsAnalizerContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<UserDto> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var refreshToken = await _dbContext.RefreshTokens
                .Include(rt => rt.User)
                .Include(rt => rt.User.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(rt => rt.Token.Equals(request.RefreshToken), cancellationToken);

            return _mapper.Map<UserDto>(refreshToken.User);
        }
    }
}
