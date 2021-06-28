﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.Core.Services.Interfaces;
using NewsAnalyzer.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsAnalyzer.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public CommentsController(ICommentService commentService,
            IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        public async Task<IActionResult> List(Guid newsId)
        {
            var comments = await _commentService.GetCommentsByNewsId(newsId);

            return View(new CommentsListViewModel
            {
                NewsId = newsId,
                Comments = comments
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentViewModel model)
        {
            var user = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimsIdentity.DefaultNameClaimType));
            var userEmail = user?.Value;
            var userId = (await _userService.GetByEmail(userEmail)).Id;

            var commentDto = new CommentDto()
            {
                Id = Guid.NewGuid(),
                NewsId = model.NewsId,
                Text = model.CommentText,
                CreateDate = DateTime.Now,
                UserId = userId
            };
            await _commentService.AddComment(commentDto);

            return Ok();
        }
    }
}
