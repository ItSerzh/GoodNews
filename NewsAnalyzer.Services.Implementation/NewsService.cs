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
using Microsoft.Extensions.Configuration;
using NewsAnalyzer.Models;
using NewsAnalyzer.Models.View;

namespace NewsAnalyzer.Services.Implementation
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public NewsService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
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
                return _mapper.Map<NewsDto>(entity);
            }
            else
            {
                return new NewsDto();
            }
        }

        public async Task<NewsListWithPaginationInfo> GetNewsBySourceId(Guid? id, int pageNumber)
        {
            var pageSize = Convert.ToInt32(_configuration["PageInfo:PageSize"]);
            var news = _unitOfWork.News.FindBy(n => true, n => n.RssSource)
               .Where(n => id == null || n.RssSourceId.Equals(id.GetValueOrDefault()));

            var newsPage = await news
               .OrderByDescending(n => n.NewsDate)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();

            var pageInfo = new PageInfo()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = news.Count()
            };

            var newsViewModel = newsPage.Select(n => _mapper.Map<NewsViewModel>(n));
            var newsListWithPaginationInfo = new NewsListWithPaginationInfo()
            {
                NewsPerPage = newsViewModel,
                PageInfo = pageInfo
            };

            return newsListWithPaginationInfo;
        }

        public async Task<NewsWithRssSourceNameDto> GetNewsWithRssSourceNameById(Guid? id)
        {
            var result = await _unitOfWork.News.FindBy(n => true, n => n.RssSource)
                .Where(n => n.Id.Equals(id.GetValueOrDefault()))
                .FirstOrDefaultAsync();
            return _mapper.Map<NewsWithRssSourceNameDto>(result);
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
