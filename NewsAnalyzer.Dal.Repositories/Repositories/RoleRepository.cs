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
    public class RoleRepository : Repository<Role>
    {
        public RoleRepository(NewsAnalyzerContext context) : base(context)
        {
        }
    }
}
