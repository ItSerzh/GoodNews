using NewsAnalyzer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsAnalyzer.Models.View;

namespace NewsAnalyzer.Core.Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> AggregateNews();
        
        Task<NewsListWithPaginationInfo> GetNewsBySourceId(Guid? id, int pageNumber);
        Task<IEnumerable<NewsWithRssSourceNameDto>> GetTopNNewsFromEachSource(int newsCount);
        Task<IEnumerable<NewsDto>> GetNewsFromRssSource(RssSourceDto rssSource);
        Task<NewsDto> GetNewsById(Guid? id);
        Task<IEnumerable<NewsWithRssSourceNameDto>> Get();
        Task<NewsWithRssSourceNameDto> GetNewsWithRssSourceNameById(Guid? id);
        Task<int> AddNews(NewsDto newsDto);
        Task Aggregate();
        Task RateNews();
        Task<IEnumerable<NewsDto>> AddRange(IEnumerable<NewsDto> newsDto);
        Task<int> Edit(Guid id);
        Task<int> Delete(Guid id);
        Task<int> Update(NewsDto newsDto);
    }
}
