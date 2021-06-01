using NewsAnalizer.Dal.Repositories;
using NewsAnalizer.Dal.Repositories.Implementation;
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
    public class RssSourceRepository : Repository<RssSource>
    {
        protected readonly NewsAnalizerContext _context;

        public RssSourceRepository(NewsAnalizerContext context) : base(context)
        {
            _context = context;
        }
    }
}
