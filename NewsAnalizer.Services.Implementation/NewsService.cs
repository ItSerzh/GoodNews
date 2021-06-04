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
using NewsAnalyzer.Utils.Html;

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
                Summary = n.Summary,
                RssSourceId = n.RssSourceId,
                Url = n.Url,
                DateCollect = n.DateCollect,
                Id = n.Id,
                Body = n.Body
            });

            await _unitOfWork.News.AddRange(news);
            await _unitOfWork.SaveChangesAsync();
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
                Summary = entity.Summary,
                DateCollect = entity.DateCollect,
                NewsDate = entity.NewsDate,
                Rating = entity.Rating,
                RssSourceId = entity.RssSourceId
            };
        }

        public async Task<IEnumerable<NewsWithRssSourceNameDto>> GetNewsBySourceId(Guid? id)
        {
            var news = _unitOfWork.News.FindBy(n => true, n => n.RssSource)
               .Where(n => !id.HasValue || n.RssSourceId.Equals(id.GetValueOrDefault()));
            
            var result = await news
               .Select(n => new NewsWithRssSourceNameDto
               {
                   Id = n.Id,
                   Title = n.Title,
                   Summary = n.Summary,
                   DateCollect = n.DateCollect,
                   NewsDate = n.NewsDate,
                   Rating = n.Rating,
                   RssSourceId = n.RssSourceId,
                   RssSourceName = n.RssSource.Name,
                   Url = n.Url,
                   Body = n.Body
               }).ToListAsync();
            return result;
        }

        public async Task<NewsWithRssSourceNameDto> GetNewsWithRssSourceNameById(Guid? id)
        {
            var result = await _unitOfWork.News.FindBy(n => true, n => n.RssSource)
                .Where(n => n.Id.Equals(id.GetValueOrDefault()))
                .Select(n => new NewsWithRssSourceNameDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Summary = n.Summary,
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

        public async Task<IEnumerable<NewsWithRssSourceNameDto>> Get()
        {
            return await _unitOfWork
                .News
                .Get()
                .Select(n => new NewsWithRssSourceNameDto
                 {
                     Id = n.Id,
                     Title = n.Title,
                     Summary = n.Summary,
                     DateCollect = n.DateCollect,
                     NewsDate = n.NewsDate,
                     Rating = n.Rating,
                     RssSourceId = n.RssSourceId
                 })
                .ToListAsync();
        }

        //public async Task<IEnumerable<NewsDto>> GetNewsFromRssSource(RssSourceDto rssSource)
        //{
        //    var newsList = new List<NewsDto>();
        //    using (var reader = XmlReader.Create(rssSource.Url))
        //    {
        //        var feed = SyndicationFeed.Load(reader);
        //        reader.Close();

        //        var feedUrls = feed.Items.Select(i => i.Id).ToList();
        //        var existingNewsUrls = await _unitOfWork.News
        //            .FindBy(news => feedUrls.Contains(news.Url) && news.RssSourceId == rssSource.Id)
        //            .Select(news => news.Url)
        //            .ToListAsync();
        //        var newFeeds = feed.Items
        //            .Where(i => !existingNewsUrls.Contains(i.Id))
        //            .ToList();

        //        foreach (var feedItem in newFeeds)
        //        {
        //            var newsDto = new NewsDto()
        //            {
        //                Id = Guid.NewGuid(),
        //                RssSourceId = rssSource.Id,
        //                Url = feedItem.Id,
        //                Title = feedItem.Title.Text,
        //                Summary = SyndicationHelper.GetSyndicationItemSummary(feedItem),
        //                NewsDate = feedItem.PublishDate.UtcDateTime,
        //                DateCollect = DateTime.UtcNow
        //            };
        //            newsList.Add(newsDto);
        //        }
        //    }
        //    return newsList;
        //}

        public async Task<IEnumerable<NewsDto>> GetNewsFromRssSource(RssSourceDto rssSource)
        {
            var newsList = new List<NewsDto>();
            using (var reader = XmlReader.Create(rssSource.Url))
            {
                var feed = SyndicationFeed.Load(reader);
                reader.Close();

                var feedUrls = feed.Items.Select(i => i.Id).ToList();
                var currentNewsUrls = await _unitOfWork.News
                    .FindBy(news => news.RssSourceId == rssSource.Id)
                    .ToListAsync();


                foreach (var feedItem in feed.Items)
                {
                    if (!currentNewsUrls.Any(news => news.Url.Equals(feedItem.Id)))
                    {
                        var newsDto = new NewsDto()
                        {
                            Id = Guid.NewGuid(),
                            RssSourceId = rssSource.Id,
                            Url = feedItem.Id,
                            Title = feedItem.Title.Text,
                            Summary = SyndicationHelper.GetSyndicationItemSummary(feedItem),
                            NewsDate = feedItem.PublishDate.UtcDateTime,
                            DateCollect = DateTime.UtcNow
                        };
                        newsList.Add(newsDto);
                    }
                }
            }
            return newsList;
        }
    }
}
