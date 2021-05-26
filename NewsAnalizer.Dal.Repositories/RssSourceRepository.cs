using NewsAnalizer.Dal.Repositories;
using NewsAnalizer.Dal.Repositories.Interfaces;
using NewsAnalizer.DAL.Core;
using NewsAnalizer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RssSourceAnalizer.Dal.Repositories.Implementation
{
    public class RssSourceRepository : IRssSourceRepository
    {
        private NewsAnalizerContext _context;

        public RssSourceRepository(NewsAnalizerContext context)
        {
            _context = context;
        }

        public async Task Add(RssSource rssSource)
        {
            _context.RssSource.Add(rssSource);
            await _context.SaveChangesAsync();
        }

        public Task AddRange(IEnumerable<RssSource> T)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<RssSource> FindBy(Expression<Func<RssSource, bool>> predicate, params Expression<Func<RssSource, object>>[] includes)
        {
            return _context.RssSource.Where(predicate);
        }

        public async Task<RssSource> GetById(Guid id)
        {
            return _context.RssSource.Find(id);
        }

        public IQueryable<RssSource> GetRssSource()
        {
            return _context.RssSource;
        }

        public async Task Remove(RssSource rssSource)
        {
            var entity = GetById(rssSource.Id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task RemoveRange(IEnumerable<RssSource> T)
        {
            throw new NotImplementedException();
        }

        public async Task Update(RssSource rssSource)
        {
            _context.Update(rssSource);
            await _context.SaveChangesAsync();
        }
    }
}
