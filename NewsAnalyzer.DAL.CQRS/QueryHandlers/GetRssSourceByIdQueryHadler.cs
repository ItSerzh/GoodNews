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
    public class GetRssSourceByIdQueryHadler : IRequestHandler<GetRssSourceByIdQuery, List<RssSourceDto>>
    {
        private NewsAnalyzerContext _context;
        private IMapper _mapper;

        public GetRssSourceByIdQueryHadler(NewsAnalyzerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RssSourceDto>> Handle(GetRssSourceByIdQuery request, CancellationToken cancellationToken)
        {
            var rssSources = await _context.RssSources.Where(s => !request.Id.HasValue  || s.Id.Equals(request.Id))
                .Select(s => _mapper.Map<RssSourceDto>(s))
                .ToListAsync(cancellationToken);
            return rssSources;
        }
    }
}
