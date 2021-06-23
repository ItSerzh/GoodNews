using MediatR;
using NewsAnalizer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.Queries
{
    public class GetTopNNotRatedNewsQuery : IRequest<List<NewsDto>>
    {
        public int Count { get; set; }
    }
}
