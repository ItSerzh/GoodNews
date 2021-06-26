using MediatR;
using NewsAnalizer.Core.DataTransferObjects;
using NewsAnalizer.Core.Services.Interfaces;
using NewsAnalyzer.DAL.CQRS.Commands;
using NewsAnalyzer.DAL.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalizer.Services.Implementation
{
    public class RefreshTokenCqsService : IRefreshTokenService
    {
         private readonly IMediator _mediator;

        public RefreshTokenCqsService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> CheckRefreshTokenIsValid(string refreshToken)
        {
            var rt = await _mediator.Send(new GetRefreshTokenByTokenValueQuery()
            { TokenValue = refreshToken });

            return rt != null && rt.ExpiresUtc >= DateTime.UtcNow;
        }

        public async Task<RefreshTokenDto> GenerateRefreshToken(Guid userId)
        {
            var newRefreshToken = new RefreshTokenDto()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                CreationDate = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddHours(1)
            };

            await _mediator.Send(new UpdateCurrentRefreshTokensCommand()
            {
                UserId = userId,
                NewRefreshToken = newRefreshToken
            });
            

            return newRefreshToken;
        }

        

    }
}
