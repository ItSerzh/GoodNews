using MediatR;
using NewsAnalyzer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.Queries
{
    public class GetRssSourceByIdQuery : IRequest<List<RssSourceDto>>
    {
        public Guid? Id { get; set; }
        public GetRssSourceByIdQuery(Guid? id)
        {
            Id = id;
        }
    }
}
