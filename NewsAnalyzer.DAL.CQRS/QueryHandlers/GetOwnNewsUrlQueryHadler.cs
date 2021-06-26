using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.QueryHandlers
{
    public class GetOwnNewsUrlQueryHadler : IRequestHandler<GetOwnNewsUrlQuery, List<string>>
    {
        private NewsAnalyzerContext _context;

        public GetOwnNewsUrlQueryHadler(NewsAnalyzerContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(GetOwnNewsUrlQuery request, CancellationToken cancellationToken)
        {
            var newsUrl = await _context.News
                .Select(s => s.Url)
               .ToListAsync(cancellationToken);
            return newsUrl;
        }
    }
}
