using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.Commands
{
    public class SetNewsRateCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public float Rating { get; set; }
    }
}
