using NewsAnalyzer.DAL.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using NewsAnalyzer.Dal.Repositories.Interfaces;

namespace NewsAnalyzer.Dal.Repositories.Interfaces
{
    public interface IRssSourceRepository : IRepository<RssSource>
    {
      //unique logic
    }
}