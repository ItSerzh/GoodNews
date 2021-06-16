﻿using NewsAnalizer.Dal.Repositories;
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
    public class UserRepository : Repository<User>
    {
        protected readonly NewsAnalizerContext _context;

        public UserRepository(NewsAnalizerContext context) : base(context)
        {
            _context = context;
        }
    }
}
