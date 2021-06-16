using NewsAnalizer.Dal.Repositories.Interfaces;
using NewsAnalizer.DAL.Core.Entities;
using System;
using System.Threading.Tasks;

namespace NewsAnalizer.Dal.Repositories.Implementation
{
    public interface IUnitOfWork : IDisposable
    {
        INewsRepository News {get;}
        IRepository<RssSource> RssSource {get;}
        IRepository<User> User { get; }
        IRepository<Role> Role { get; }


        Task<int> SaveChangesAsync();
    }
}