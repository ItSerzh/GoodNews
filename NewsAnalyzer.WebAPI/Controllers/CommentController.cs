using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.Core.Services.Interfaces;
using NewsAnalyzer.WebAPI.Controllers.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsAnalyzer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        public CommentController(ICommentService commentService, IUserService userService)
        {
            _commentService = commentService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var news = await _commentService.GetCommentsByNewsId(id);
            return Ok(news);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CommentRequest request)
        {
            var userEmail = HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            var user = await _userService.GetByEmail(userEmail);

            var commentDto = new CommentDto()
            {
                NewsId = request.NewsId,
                Text = request.Text,
                UserId = user.Id
            };
            var result = await _commentService.AddComment(commentDto);

            if (result == 1)
            {
                return Ok("Comment added");
            }
            else
            {
                return StatusCode(500, "Comment was not added.");
            }
        }
    }
}
