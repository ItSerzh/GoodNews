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
using MediatR;
using NewsAnalyzer.DAL.CQRS.Queries;
using NewsAnalyzer.DAL.CQRS.Commands;

namespace NewsAnalyzer.Services.Implementation
{
    public class CommentCqsService : ICommentService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CommentCqsService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<int> AddComment(CommentDto commentDto)
        {
            var command = new AddCommentCommand()
            {
                Id = commentDto.Id,
                NewsId = commentDto.NewsId,
                Text = commentDto.Text,
                UserId = commentDto.UserId
            };
            return await _mediator.Send(command);
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByNewsId(Guid id)
        {
            var commentsQuery = new GetCommentsByNewsIdQuery() { Id = id };

            var comments = await _mediator.Send(commentsQuery);
                
            return comments.Select(c => _mapper.Map<CommentDto>(c)).ToList();
        }
    }
}
