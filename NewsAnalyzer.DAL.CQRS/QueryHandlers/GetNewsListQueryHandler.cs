using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;
using NewsAnalyzer.DAL.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.QueryHandlers
{
    public class GetNewsListQueryHandler : IRequestHandler<GetNewsListQuery, List<NewsDto>>
    {
        private readonly NewsAnalyzerContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public GetNewsListQueryHandler(NewsAnalyzerContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<NewsDto>> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
        {
            var pageSize = Convert.ToInt32(_configuration["PageInfo:PageSize"]);
            var news = await _dbContext.News
                .OrderByDescending(n => n.NewsDate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
                
            return news.Select(n => _mapper.Map<NewsDto>(n)).ToList();
        }
    }
}
