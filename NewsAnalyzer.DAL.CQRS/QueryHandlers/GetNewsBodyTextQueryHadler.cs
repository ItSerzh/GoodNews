﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.CQRS.Queries;
using NewsAnalyzer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.QueryHandlers
{
    public class GetNewsBodyTextQueryHadler : IRequestHandler<GetNewsBodyTextQuery, string>
    {
        private NewsAnalyzerContext _dbContext;

        public GetNewsBodyTextQueryHadler(NewsAnalyzerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(GetNewsBodyTextQuery request, CancellationToken cancellationToken)
        {
            var newsBody = (await _dbContext.News
                .FirstOrDefaultAsync(n => n.Id.Equals(request.Id),cancellationToken))
                .Body;
            return HtmlCleanup.RemoveHtmlTags(newsBody);
        }
    }
}
