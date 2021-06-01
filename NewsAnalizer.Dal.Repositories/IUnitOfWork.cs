using NewsAnalizer.Dal.Repositories.Interfaces;
using NewsAnalizer.DAL.Core.Entities;
using System;
using System.Threading.Tasks;

namespace NewsAnalizer.Dal.Repositories.Implementation
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<News> News {get;}
        IRepository<RssSource> RssSource {get;}

        Task<int> SaveChangesAsync();
    }
}