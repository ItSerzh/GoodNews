using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAnalizer.Core.DataTransferObjects;
using NewsAnalizer.DAL.Core;
using NewsAnalyzer.DAL.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.QueryHandlers
{
    public class GetTopNNotRatedNewsQueryHadler : IRequestHandler<GetTopNNotRatedNewsQuery, List<NewsDto>>
    {
        private NewsAnalizerContext _dbContext;
        private IMapper _mapper;

        public GetTopNNotRatedNewsQueryHadler(NewsAnalizerContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<NewsDto>> Handle(GetTopNNotRatedNewsQuery request, CancellationToken cancellationToken)
        {
            var news = await _dbContext.News.AsNoTracking()
                .Where(n => n.Rating == null)
                .OrderBy(n => n.NewsDate)
                .Select(n => _mapper.Map<NewsDto>(n))
                .Take(request.Count)
                .ToListAsync();
            return news;
        }
    }
}
