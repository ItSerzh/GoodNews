using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAnalizer.DAL.Core;
using NewsAnalyzer.DAL.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.CommandHandlers
{
    public class SetNewsRateCommandHandler : IRequestHandler<SetNewsRateCommand, int>
    {
        private NewsAnalizerContext _dbContext;

        public SetNewsRateCommandHandler(NewsAnalizerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(SetNewsRateCommand request, CancellationToken cancellationToken)
        {
            var news = await _dbContext.News.FirstOrDefaultAsync(n => n.Id.Equals(request.Id));
            news.Rating = request.Rating;
            return await _dbContext.SaveChangesAsync();
        }
    }
}
