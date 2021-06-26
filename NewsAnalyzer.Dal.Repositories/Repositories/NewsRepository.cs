using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.Dal.Repositories.Interfaces;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace NewsAnalyzer.Dal.Repositories.Implementation
{
    public class NewsRepository : Repository<News>, INewsRepository
    {
        protected readonly NewsAnalyzerContext _context;
        protected readonly DbSet<News> _table;
        protected readonly IMapper _mapper;

        public NewsRepository(NewsAnalyzerContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task <IEnumerable<NewsWithRssSourceNameDto>> GetTopNNewsFromEachSource(int count)
        {
            var rssSources = _context.RssSources.ToList();

            var retVal = new List<NewsWithRssSourceNameDto>();

            foreach (var rssSource in rssSources)
            {
                var topNNews = await _context.News
                    .Where(n => n.RssSourceId == rssSource.Id)
                    .Include(n => n.RssSource)
                    .OrderByDescending(n => n.NewsDate)
                    .Select(n =>  _mapper.Map<News, NewsWithRssSourceNameDto>(n))
                    .Take(count)
                    .ToListAsync();

                retVal.AddRange(topNNews);
            }

            return retVal;
        }
    }
}
