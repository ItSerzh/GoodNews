using MediatR;
using NewsAnalyzer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.Commands
{
    public class UpdateCurrentRefreshTokensCommand : IRequest<int>
    {
        public Guid UserId { get; set; }
        public RefreshTokenDto NewRefreshToken {get;set;}
    }
}
