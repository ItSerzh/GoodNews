using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;
using NewsAnalyzer.DAL.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.CommandHandlers
{
    public class UpdateCurrentRefreshTokensCommandHandler : IRequestHandler<UpdateCurrentRefreshTokensCommand, int>
    {
        private NewsAnalyzerContext _dbContext;
        private IMapper _mapper;

        public UpdateCurrentRefreshTokensCommandHandler(NewsAnalyzerContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateCurrentRefreshTokensCommand request, CancellationToken cancellationToken)
        {
            var dbTokens = await _dbContext.RefreshTokens.Where(r => r.UserId.Equals(request.UserId)).ToArrayAsync();

            _dbContext.RefreshTokens.RemoveRange(dbTokens);
            await _dbContext.SaveChangesAsync();

            await _dbContext.AddAsync(_mapper.Map<RefreshToken>(request.NewRefreshToken), cancellationToken);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
