using NewsAnalizer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAnalizer.Dal.Repositories.Interfaces;

namespace NewsAnalizer.Dal.Repositories.Interfaces
{
    public interface INewsRepository : IRepository<News>
    {
        //unique logic
    }
}