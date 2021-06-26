using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAnalyzer.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAnalyzer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RssSourceSontroller : ControllerBase
    {
        private readonly IRssSourceService _rssSourceService;
        public RssSourceSontroller(IRssSourceService rssSourceService)
        {
            _rssSourceService = rssSourceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var news = await _rssSourceService.GetRssSources(id);
            return Ok(news);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var news = await _rssSourceService.GetRssSources(null);
            return Ok(news);
        }
    }
}
