using NewsAnalyzer.Dal.Repositories.Interfaces;
using NewsAnalyzer.DAL.Core.Entities;
using System;
using System.Threading.Tasks;

namespace NewsAnalyzer.Dal.Repositories.Implementation
{
    public interface IUnitOfWork : IDisposable
    {
        INewsRepository News {get;}
        IRepository<Comment> Comment { get; }
        IRepository<RssSource> RssSource {get;}
        IRepository<User> User { get; }
        IRepository<Role> Role { get; }

        Task<int> SaveChangesAsync();
    }
}