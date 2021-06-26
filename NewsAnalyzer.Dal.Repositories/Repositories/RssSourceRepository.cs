using NewsAnalyzer.Dal.Repositories;
using NewsAnalyzer.Dal.Repositories.Implementation;
using NewsAnalyzer.Dal.Repositories.Interfaces;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RssSourceAnalyzer.Dal.Repositories.Implementation
{
    public class RssSourceRepository : Repository<RssSource>
    {
        protected readonly NewsAnalyzerContext _context;

        public RssSourceRepository(NewsAnalyzerContext context) : base(context)
        {
            _context = context;
        }
    }
}
