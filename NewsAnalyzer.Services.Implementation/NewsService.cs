using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.Core.Interfaces.Services;
using NewsAnalyzer.Core.Services.Interfaces;
using NewsAnalyzer.Dal.Repositories.Interfaces;
using NewsAnalyzer.Dal.Repositories.Implementation;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using NewsAnalyzer.Utils.Html;
using Serilog;
using AutoMapper;

namespace NewsAnalyzer.Services.Implementation
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            var news = newsDto.Select(n => _mapper.Map<News>(n));

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
            if (entity != null)
            {
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
            else
            {
                return new NewsDto();
            }
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

                Log.Information($"News count in DB: {currentNewsUrls.Count}");

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

        public async Task<IEnumerable<NewsWithRssSourceNameDto>> GetTopNNewsFromEachSource(int newsCount)
        {
            var retVal = await _unitOfWork.News.GetTopNNewsFromEachSource(newsCount);

            return retVal;
        }

        public Task Aggregate()
        {
            throw new NotImplementedException();
        }

        public Task RateNews()
        {
            throw new NotImplementedException();
        }
    }
}
