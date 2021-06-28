using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;
using NewsAnalyzer.DAL.CQRS.Commands;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.CommandHandlers
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, int>
    {
        private NewsAnalyzerContext _dbContext;

        public AddCommentCommandHandler(NewsAnalyzerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var comment = new Comment()
                {
                    Id = request.Id,
                    CreateDate = DateTime.Now,
                    NewsId = request.NewsId,
                    Text = request.Text,
                    UserId = request.UserId
                };

                await _dbContext.AddAsync(comment);
                
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(e, $"Error has occured durin comment insertion newsId: " +
                    $"{request.NewsId}, userId: {request.UserId}, text: {request.Text}");
                return 0;
            }
        }
    }
}
