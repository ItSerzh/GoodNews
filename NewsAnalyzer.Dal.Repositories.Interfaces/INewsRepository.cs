using NewsAnalyzer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAnalyzer.Dal.Repositories.Interfaces;
using NewsAnalyzer.Core.DataTransferObjects;

namespace NewsAnalyzer.Dal.Repositories.Interfaces
{
    public interface INewsRepository : IRepository<News>
    {
        public Task<IEnumerable<NewsWithRssSourceNameDto>> GetTopNNewsFromEachSource(int count);
    }
}