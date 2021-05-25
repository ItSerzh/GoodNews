using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsAnalizer.Core.Services.Interfaces;
using NewsAnalizer.Dal.Repositories.Implementation;
using NewsAnalizer.DAL.Core;
using NewsAnalizer.DAL.Core.Entities;

namespace NewsAnalyzer.Controllers
{
    public class RssSourcesController : Controller
    {
        //private readonly NewsAnalizerContext _context;
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

            return View(rssSource);
        }

        // GET: RssSources/Create
        public IActionResult Create()
        {
            return View();
        }

    }
}
