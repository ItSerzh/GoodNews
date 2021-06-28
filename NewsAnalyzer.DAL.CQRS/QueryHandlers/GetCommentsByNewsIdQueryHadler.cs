using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.CQRS.Queries;
using NewsAnalyzer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.QueryHandlers
{
    public class GetCommentsByNewsIdQueryHadler : IRequestHandler<GetCommentsByNewsIdQuery, List<CommentDto>>
    {
        private NewsAnalyzerContext _dbContext;
        private IMapper _mapper;

        public GetCommentsByNewsIdQueryHadler(NewsAnalyzerContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<CommentDto>> Handle(GetCommentsByNewsIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await _dbContext.Comments
                .Where(c => c.NewsId.Equals(request.Id))
                .ToListAsync();

            return comments.Select(c => _mapper.Map<CommentDto>(c)).ToList();
        }
    }
}
