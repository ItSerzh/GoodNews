using NewsAnalizer.DAL.Core.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using NewsAnalizer.Dal.Repositories.Interfaces;

namespace NewsAnalizer.Dal.Repositories.Interfaces
{
    public interface IRssSourceRepository : IRepository<RssSource>
    {
      //unique logic
    }
}