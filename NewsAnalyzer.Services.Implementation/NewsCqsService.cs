﻿using Microsoft.EntityFrameworkCore;
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
using MediatR;
using NewsAnalyzer.DAL.CQRS.Queries;
using System.Collections.Concurrent;
using NewsAnalyzer.Web.Services;
using NewsAnalyzer.Helpers;
using Microsoft.Extensions.Configuration;
using NewsAnalyzer.Util.Text;
using NewsAnalyzer.DAL.CQRS.Commands;
using NewsAnalyzer.Models.View;
using NewsAnalyzer.Models;

namespace NewsAnalyzer.Services.Implementation
{
    public class NewsCqsService : INewsService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public NewsCqsService(IMediator mediator, IMapper mapper, IConfiguration configuration)
        {
            _mediator = mediator;
            _mapper = mapper;
            _configuration = configuration;
        }

        public NewsCqsService()
        {
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


        public async Task<NewsDto> GetNewsById(Guid? id)
        {
            var newsByIdQuery = new GetNewsByIdQuery() { Id = id.Value };
            var retVal = await _mediator.Send(newsByIdQuery);
            return retVal;
        }

        public async Task<IEnumerable<NewsWithRssSourceNameDto>> GetNewsBySourceId(Guid? id)
        {
            throw new NotImplementedException();
        }

        public async Task<NewsWithRssSourceNameDto> GetNewsWithRssSourceNameById(Guid? id)
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

        public async Task<IEnumerable<NewsWithRssSourceNameDto>> Get()
        {
            throw new NotImplementedException();

        }


        public async Task<IEnumerable<NewsDto>> GetNewsFromRssSource(RssSourceDto rssSource)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NewsWithRssSourceNameDto>> GetTopNNewsFromEachSource(int newsCount)
        {
            throw new NotImplementedException();
        }

        public async Task Aggregate()
        {
            var query = new GetRssSourceByIdQuery(null);
            var rssSources = await _mediator.Send(query);

            var newsList = new ConcurrentBag<NewsDto>();

            var currentNewsUrls = await _mediator.Send(new GetOwnNewsUrlQuery());

            Parallel.ForEach(rssSources, (rssSource) =>
            {
                using (var reader = XmlReader.Create(rssSource.Url))
                {
                    var feed = SyndicationFeed.Load(reader);
                    reader.Close();

                    var feedUrls = feed.Items.Select(i => i.Id).ToList();


                    Log.Information($"News count in DB: {currentNewsUrls.Count}");

                    foreach (var feedItem in feed.Items)
                    {
                        if (!currentNewsUrls.Any(url => url.Equals(feedItem.Id)))
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
            });
            //return newsList;


        }

        public async Task RateNews()
        {
            var topNNotratedNewsQuery = new GetTopNNotRatedNewsQuery() { Count = 30 };
            var topNotRatedNews = await _mediator.Send(topNNotratedNewsQuery);
            var affinList = Text.ReadAfinn(_configuration["AfinnPath"]);

            foreach(var news in topNotRatedNews)
            {
                if (!string.IsNullOrEmpty(news.Body))
                {
                    var clearBodyText = HtmlCleanup.RemoveHtmlTags(news.Body);
                    var lemmaList = (await Ispras.GetTexterra(
                        Text.PrepareForIspras(clearBodyText)))
                        .Where(l => !string.IsNullOrEmpty(l));

                    var lemmaWithRate = lemmaList.Select(l => new
                    {
                        Lemma = l,
                        Rate = affinList.Where(aff => aff.Key == l)
                            .FirstOrDefault().Value
                    });
                    var rate = lemmaWithRate.Where(r => r.Rate != null)
                    .Average(r => r.Rate);

                    if (rate == null)
                    {
                        rate = 0;
                        Log.Debug($"News with id {news.Id} rate is null !!!");
                    }
                    var setNewsRateCommand = new SetNewsRateCommand() { Id = news.Id, Rating = (float)rate.Value };
                    var updatedCount = await _mediator.Send(setNewsRateCommand);

                    Log.Information($"Set rate for {topNotRatedNews.Count()} news");
                }
                else
                {
                    Log.Error($"News with id {news.Id} has empty body");
                }
            }
        }

        public async Task<NewsListWithPaginationInfo> GetNewsBySourceId(Guid? id, int pageNumber)
        {
            var pageSize = Convert.ToInt32(_configuration["PageInfo:PageSize"]);
            var query = new GetNewsListQuery();
            var news = await _mediator.Send(query);

            var newsPage = news
               .OrderByDescending(n => n.NewsDate)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToList();

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
    }
}
