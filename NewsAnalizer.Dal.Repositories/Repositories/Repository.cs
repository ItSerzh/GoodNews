using Microsoft.EntityFrameworkCore;
using NewsAnalizer.Dal.Repositories.Interfaces;
using NewsAnalizer.DAL.Core;
using NewsAnalizer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalizer.Dal.Repositories.Implementation
{
    public abstract class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        protected readonly NewsAnalizerContext Db;
        protected readonly DbSet<T> Table;

        protected Repository(NewsAnalizerContext db)
        {
            Db = db;
            Table = Db.Set<T>();
        }


        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = Table.Where(predicate);
            if (includes.Any())
            {
                result = includes
                    .Aggregate(result, (current, include) => current.Include(include));
            }
            return result;
        }

        public Task<T> GetById(Guid id)
        {
            return Table.FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public DbSet<T> Get()
        {
            return Table;
        }

        public void Add(T entity)
        {
             Db.Add(entity);
        }

        public async Task AddRange(IEnumerable<T> T)
        {
            await Db.AddRangeAsync(T);
        }

        public Task Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRange(IEnumerable<T> T)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Db?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
