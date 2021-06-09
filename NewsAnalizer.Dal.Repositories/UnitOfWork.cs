using NewsAnalizer.Dal.Repositories.Interfaces;
using NewsAnalizer.DAL.Core;
using NewsAnalizer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalizer.Dal.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly NewsAnalizerContext _db;
        private readonly INewsRepository _newsRepository;
        private readonly IRepository<RssSource> _rssRepository;

        public UnitOfWork(NewsAnalizerContext db, INewsRepository newsRepository, IRepository<RssSource> rssRepository)
        {
            _db = db;
            _newsRepository = newsRepository;
            _rssRepository = rssRepository;
        }

        public INewsRepository News => _newsRepository;
        public IRepository<RssSource> RssSource => _rssRepository;

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
        public void Dispose()
        {
            _db?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
