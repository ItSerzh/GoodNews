using Microsoft.EntityFrameworkCore;
using NewsAnalizer.Core.DataTransferObjects;
using NewsAnalizer.Core.Interfaces.Services;
using NewsAnalizer.DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAnalizer.Services.Implementation
{
    public class NewsService : INewsService
    {
        private NewsAnalizerContext _db;

        public NewsService(NewsAnalizerContext db)
        {
            _db = db;
        }

        public Task<NewsDto> AddNews(NewsDto newsDto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NewsDto>> AddRange(IEnumerable<NewsDto> newsDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(NewsDto newsDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Edit(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NewsDto>> FindNews()
        {
            throw new NotImplementedException();
        }

        public Task<NewsDto> GetNewsById(Guid? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NewsDto>> GetNewsBySourceId(Guid? id)
        {
            var news = await _db.News
                .Where(n => n.RssSourceId == id)
                .Select(n => new NewsDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    DateCollect = n.DateCollect,
                    NewsDate = n.NewsDate,
                    Rating = n.Rating,
                    RssSourceId = n.RssSourceId
                })
                .ToListAsync();
            return news;
        }

        public Task<NewsWithRssSourceNameDto> GetNewsWithRssSourceNameById(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(NewsDto newsDto)
        {
            throw new NotImplementedException();
        }

        Task<int> INewsService.AddNews(NewsDto newsDto)
        {
            throw new NotImplementedException();
        }
    }
}
