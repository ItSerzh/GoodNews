using NewsAnalizer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAnalizer.Dal.Repositories.Interfaces;
using NewsAnalizer.Core.DataTransferObjects;

namespace NewsAnalizer.Dal.Repositories.Interfaces
{
    public interface INewsRepository : IRepository<News>
    {
        public Task<IEnumerable<NewsWithRssSourceNameDto>> GetTopNNewsFromEachSource(int count);
    }
}