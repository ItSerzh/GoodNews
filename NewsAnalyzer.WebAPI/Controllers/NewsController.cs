﻿using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var news = await _newsService.GetNewsById(id);
            return Ok(news);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid? sourceId, int? pageNumber = 1)
        {
            var news = await _newsService.GetNewsBySourceId(sourceId, pageNumber.Value);
            return Ok(news);
        }
    }
}
