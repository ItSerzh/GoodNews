using MediatR;
using NewsAnalyzer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.Commands
{
    public class AddCommentCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid NewsId { get; set; }
        public Guid UserId { get; set; }
    }
}
