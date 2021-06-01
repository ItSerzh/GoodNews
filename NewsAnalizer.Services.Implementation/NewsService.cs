using Microsoft.EntityFrameworkCore;
using NewsAnalizer.Core.DataTransferObjects;
using NewsAnalizer.Core.Interfaces.Services;
using NewsAnalizer.Core.Services.Interfaces;
using NewsAnalizer.Dal.Repositories.Interfaces;
using NewsAnalizer.Dal.Repositories.Implementation;
using NewsAnalizer.DAL.Core;
using NewsAnalizer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace NewsAnalizer.Services.Implementation
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<NewsDto>> AggregateNews()
        {
            throw new NotImplementedException();
        }


        public Task<NewsDto> AddNews(NewsDto newsDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NewsDto>> AddRange(IEnumerable<NewsDto> newsDto)
        {
            var news = newsDto.Select(n => new News
            {
                Title = n.Title,
                NewsDate = n.NewsDate,
                Content = n.Content,
                RssSourceId = n.RssSourceId,
                Url = n.Url,
            });
            
            //_repository.AddRange(news);
            //await _db.SaveChangesAsync();
            return newsDto;
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


        public async Task<NewsDto> GetNewsById(Guid? id)
        {
            var entity = await _unitOfWork.News.GetById(id.Value);
            return new NewsDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Content = entity.Content,
                DateCollect = entity.DateCollect,
                NewsDate = entity.NewsDate,
                Rating = entity.Rating,
                RssSourceId = entity.RssSourceId
            };
        }

        public async Task<IEnumerable<NewsWithRssSourceNameDto>> GetNewsBySourceId(Guid? id)
        {
            var v = _unitOfWork.News.FindBy(n => true, n => n.RssSource)
               .Where(n => n.RssSourceId.Equals(id.GetValueOrDefault()));
            var result = await v   
               .Select(n => new NewsWithRssSourceNameDto
               {
                   Id = n.Id,
                   Title = n.Title,
                   Content = n.Content,
                   DateCollect = n.DateCollect,
                   NewsDate = n.NewsDate,
                   Rating = n.Rating,
                   RssSourceId = n.RssSourceId,
                   RssSourceName = n.RssSource.Name,
                   Url = n.Url
               }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<NewsDto>> GetNewsFromRssSource(RssSourceDto rssSource)
        {
            var newsList = new List<NewsDto>();
            using (var reader = XmlReader.Create(rssSource.Url))
            {
                var feed = SyndicationFeed.Load(reader);

                reader.Close();

                if (feed.Items.Any())
                {
                    var dbUrls = await _unitOfWork.News.Get()
                        .Where(n => n.RssSourceId == rssSource.Id)
                        .Select(n => n.Url)
                        .ToListAsync();

                    foreach (var item in feed.Items)
                    {
                        if (!dbUrls.Contains(item.Id))
                        {
                            var newsDto = new NewsDto()
                            {
                                Id = Guid.NewGuid(),
                                RssSourceId = rssSource.Id,
                                Url = item.Id,
                                Title = item.Title.Text,
                                Content = item.Summary.Text //item.Content.ToString()
                            };
                            newsList.Add(newsDto);
                        }
                    }
                }
            }
            return newsList;
        }

        public async Task<NewsWithRssSourceNameDto> GetNewsWithRssSourceNameById(Guid? id)
        {
            var result = await _unitOfWork.News.FindBy(n => true, n => n.RssSource)
                .Where(n => n.Id.Equals(id.GetValueOrDefault()))
                .Select(n => new NewsWithRssSourceNameDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    DateCollect = n.DateCollect,
                    NewsDate = n.NewsDate,
                    Rating = n.Rating,
                    RssSourceId = n.RssSourceId
                }).FirstOrDefaultAsync();
            return result;
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
