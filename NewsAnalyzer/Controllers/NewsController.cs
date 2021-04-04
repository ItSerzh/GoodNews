using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsAnalizer.Core.DataTransferObjects;
using NewsAnalizer.Core.Interfaces.Services;
using NewsAnalizer.DAL.Core;
using NewsAnalizer.DAL.Core.Entities;
using NewsAnalyzer.Models.ViewModels;

namespace NewsAnalyzer.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // GET: News
        public async Task<IActionResult> Index(Guid? rssSourceId)
        {
            if (rssSourceId == null)
            {
                return NotFound();
            }

            var news = (await _newsService.GetNewsBySourceId(rssSourceId)).ToList();
            return View(news);
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
                Content = news.Content,
                Url = news.Url,
                NewsDate = news.NewsDate,
                DateCollect = news.DateCollect,
                RssSourceId = news.RssSourceId,
                RssSourceName = news.RssSourceName
            };

            return View(vewModel);
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
