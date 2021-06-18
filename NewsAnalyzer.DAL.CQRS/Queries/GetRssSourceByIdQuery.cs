using MediatR;
using NewsAnalizer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.Queries
{
    public class GetRssSourceByIdQuery : IRequest<IEnumerable<RssSourceDto>>
    {
        public Guid? Id { get; set; }
        public GetRssSourceByIdQuery(Guid? id)
        {
            Id = id;
        }
    }
}
