using NewsAnalizer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsAnalizer.Core.Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> AggregateNews();
        
        Task <IEnumerable<NewsWithRssSourceNameDto>> GetNewsBySourceId(Guid? id);
        Task<IEnumerable<NewsDto>> GetNewsFromRssSource(RssSourceDto rssSource);
        Task<NewsDto> GetNewsById(Guid? id);
        Task<NewsWithRssSourceNameDto> GetNewsWithRssSourceNameById(Guid? id);
        Task<int> AddNews(NewsDto newsDto);
        Task<IEnumerable<NewsDto>> AddRange(IEnumerable<NewsDto> newsDto);
        Task<int> Edit(Guid id);
        Task<int> Delete(Guid id);
        Task<int> Update(NewsDto newsDto);
    }
}
