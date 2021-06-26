using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAnalizer.Core.DataTransferObjects;
using NewsAnalizer.DAL.Core;
using NewsAnalizer.DAL.Core.Entities;
using NewsAnalyzer.DAL.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.QueryHandlers
{
    public class GetRefreshTokenByTokenValueQueryHandler : IRequestHandler<GetRefreshTokenByTokenValueQuery, RefreshTokenDto>
    {
        private NewsAnalizerContext _dbContext;
        private IMapper _mapper;

        public GetRefreshTokenByTokenValueQueryHandler(NewsAnalizerContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<RefreshTokenDto> Handle(GetRefreshTokenByTokenValueQuery request, CancellationToken cancellationToken)
        {
            var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(n => n.Token.Equals(request.TokenValue));

            return _mapper.Map<RefreshTokenDto>(refreshToken);
        }
    }
}
