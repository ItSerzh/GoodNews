using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.Core.Services.Interfaces;
using NewsAnalyzer.Dal.Repositories.Implementation;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;

namespace NewsAnalyzer.Controllers
{
    public class RssSourcesController : Controller
    {
        //private readonly NewsAnalyzerContext _context;
        private readonly IRssSourceService _rssService;

        public RssSourcesController(IRssSourceService rssService)
        {
            _rssService = rssService;
        }

        // GET: RssSources
        public async Task<IActionResult> Index()
        {
            return View(await _rssService.GetRssSources());
        }

        // GET: RssSources/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rssSource = await _rssService.GetRssSources(id);
                
            if (rssSource == null)
            {
                return NotFound();
            }

            return View(rssSource.FirstOrDefault());
        }

        // GET: RssSources/Create
        public IActionResult Create()
        {
            return View();
        }

    }
}
