using MediatR;
using NewsAnalizer.DAL.Core;
using NewsAnalizer.DAL.Core.Entities;
using NewsAnalyzer.DAL.CQRS.Commands;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAnalyzer.DAL.CQRS.CommandHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly NewsAnalizerContext _dbContext;

        public RegisterUserCommandHandler(NewsAnalizerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PasswordHash = request.PasswordHash,
                    RoleId = request.RoleId
                };

                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Error has occured durin registration user: {request.Email}");
                return false;
            }
        }
    }
}
