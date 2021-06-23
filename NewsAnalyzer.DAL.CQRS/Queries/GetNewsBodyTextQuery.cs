using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.Queries
{
    public class GetNewsBodyTextQuery : IRequest<string>
    {
        public Guid Id { get; set; }
    }
}
