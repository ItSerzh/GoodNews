using NewsAnalizer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsAnalizer.Core.Interfaces.Services
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> FindNews();
        
        Task <IEnumerable<NewsDto>> GetNewsBySourceId(Guid? id);
        Task<NewsDto> GetNewsById(Guid? id);
        Task<NewsWithRssSourceNameDto> GetNewsWithRssSourceNameById(Guid? id);
        Task<int> AddNews(NewsDto newsDto);
        Task<IEnumerable<NewsDto>> AddRange(IEnumerable<NewsDto> newsDto);
        Task<int> Edit(Guid id);
        Task<int> Delete(Guid id);
        Task<int> Update(NewsDto newsDto);
    }
}
