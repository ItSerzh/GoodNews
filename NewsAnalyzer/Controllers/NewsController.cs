using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.Core.Interfaces.Services;
using NewsAnalyzer.Core.Services.Interfaces;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;
using NewsAnalyzer.Services.Implementation;
using NewsAnalyzer.Helpers;
using NewsAnalyzer.Models.ViewModels;
using NewsAnalyzer.Utils.Html;
using Serilog;
using static NewsAnalyzer.Services.Implementation.WebPageParse;
using AutoMapper;
using NewsAnalyzer.Models;
using NewsAnalyzer.Models.View;

namespace NewsAnalyzer.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IRssSourceService _rssSourceService;
        private readonly IgromaniaParser _igromaniaParser;
        private readonly ShazooParser _shazooParser;
        private readonly OnlinerParser _onlinerParser;
        private readonly ForPdaParser _4PdaParser;
        private readonly WylsaParser _wylsaParser;
        private readonly IMapper _mapper;

        public NewsController(INewsService newsService, IRssSourceService rssSourceService,
            IgromaniaParser igromaniaParser, ShazooParser shazooParser,
            OnlinerParser onlinerParser, ForPdaParser forPdaParser, WylsaParser wylsaParser, IMapper mapper)
        {
            _newsService = newsService;
            _rssSourceService = rssSourceService;
            _igromaniaParser = igromaniaParser;
            _shazooParser = shazooParser;
            _onlinerParser = onlinerParser;
            _4PdaParser = forPdaParser;
            _wylsaParser = wylsaParser;
            _mapper = mapper;
        }

        // GET: News
        [Authorize]
        public async Task<IActionResult> Index(Guid? rssSourceId, int page )
        {
            var news = await _newsService.GetNewsBySourceId(rssSourceId);
            //var news = await _newsService.GetTopNNewsFromEachSource(3);

            //move to service
            int pageSize = 15;
            var pageNews = news.Skip(pageSize * (page - 1)).Take(pageSize);
            var pageInfo = new PageInfo()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = news.Count()
            };

            var newsViewModel = pageNews.Select(n => _mapper.Map<NewsViewModel>(n));
            var newsListWithPaginationInfo = new NewsListWithPaginationInfo()
            {
                NewsPerPage = newsViewModel,
                PageInfo = pageInfo
            };

            //return View(news.Select(n => _mapper.Map<NewsViewModel>(n)).ToList());
            return View(newsListWithPaginationInfo);
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _newsService.GetNewsWithRssSourceNameById(id);

            var vewModel = new NewsWithRssSourceNameDto
            {
                Id = news.Id,
                Title = news.Title,
                Summary = news.Summary,
                Url = news.Url,
                NewsDate = news.NewsDate,
                DateCollect = news.DateCollect,
                RssSourceId = news.RssSourceId,
                RssSourceName = news.RssSourceName
            };

            return View(vewModel);
        }

        public IActionResult Aggregate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aggregate([Bind("Id,Title,Content,Url,Rating,NewsDate,DateCollect,RssSourceId")] NewsDto news)
        {
            try
            {
                var rssSources = (await _rssSourceService.GetRssSources());
                Log.Information("***Try to aggregate news");
                var allSourcesNews = new List<NewsDto>();
                foreach (var rssSource in rssSources)
                {
                    Log.Information($"Aggregate from {rssSource.Name}");
                    var oneSourceNewsList = await _newsService.GetNewsFromRssSource(rssSource);
                    Log.Information($"News in rss source: {oneSourceNewsList.Count()}");

                    foreach (var oneSourceNews in oneSourceNewsList)
                    {
                        if (rssSource.Name == "Igromania")
                        {
                            oneSourceNews.Body = await _igromaniaParser.Parse(oneSourceNews.Url);
                        }
                        if (rssSource.Name == "Shazoo")
                        {
                            oneSourceNews.Body = await _shazooParser.Parse(oneSourceNews.Url);
                        }
                        if (rssSource.Name == "Onliner")
                        {
                            oneSourceNews.Body = await _onlinerParser.Parse(oneSourceNews.Url);
                        }
                        if (rssSource.Name == "4Pda")
                        {
                            oneSourceNews.Body = await _4PdaParser.Parse(oneSourceNews.Url);
                        }
                        if (rssSource.Name == "Wylsa")
                        {
                            oneSourceNews.Body = await _wylsaParser.Parse(oneSourceNews.Url);
                        }
                    }
                    
                    allSourcesNews.AddRange(oneSourceNewsList);
                    Log.Information($"Finished aggregate from {rssSource.Name}");
                }

                Log.Information($"***Total aggregated news count {allSourcesNews.Count()}");
                await _newsService.AddRange(allSourcesNews);
            }
            catch (Exception e)
            {
                Log.Error(e, $"Failed to aggregate news ");
                throw; 
            }
            return RedirectToAction(nameof(Index),(new Guid("E3512D7D-381A-4655-8B60-584C08D9254A")));
        }

        // GET: News/Create
        public IActionResult Create()
        {
            //ViewData["RssSourceId"] = new SelectList(_newsService. RssSource, "Id", "Id");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Url,Rating,NewsDate,DateCollect,RssSourceId")] NewsDto news)
        {
            if (ModelState.IsValid)
            {
                await _newsService.AddNews(news);

                return RedirectToAction(nameof(Index));
            }
            //ViewData["RssSourceId"] = new SelectList(_newsService.RssSource, "Id", "Id", news.RssSourceId);
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _newsService.GetNewsById(id.Value);
            if (news == null)
            {
                return NotFound();
            }
            //ViewData["RssSourceId"] = new SelectList(_newsService.RssSource, "Id", "Id", news.RssSourceId);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Content,Url,Rating,NewsDate,DateCollect,RssSourceId")] NewsDto news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _newsService.Update(news);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["RssSourceId"] = new SelectList(_newsService.RssSource, "Id", "Id", news.RssSourceId);
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _newsService.GetNewsById(id);

            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var news = await _newsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(Guid id)
        {
            return _newsService.GetNewsById(id).Result != null;
        }
    }
}
