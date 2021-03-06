﻿using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Dal.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class, IBaseEntity
    {
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> GetById(Guid id);
        DbSet<T> Get();
        Task Add(T entity);
        Task AddRange(IEnumerable<T> T);
        Task Update(T entity);
        Task Remove(T entity);
        Task RemoveRange(IEnumerable<T> T);
    }
}
