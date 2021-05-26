using Microsoft.EntityFrameworkCore;
using NewsAnalizer.Dal.Repositories.Interfaces;
using NewsAnalizer.DAL.Core;
using NewsAnalizer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NewsAnalizer.Dal.Repositories.Implementation
{
    public class NewsRepository : Repository<News>
    {
        protected readonly NewsAnalizerContext _context;
        protected readonly DbSet<News> _table;
        public NewsRepository(NewsAnalizerContext context ) : base(context)
        {
            _context = context;
            //_table = table;
            
        }
    }
}
