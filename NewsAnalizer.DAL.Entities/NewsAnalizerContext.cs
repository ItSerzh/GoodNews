﻿using Microsoft.EntityFrameworkCore;
using NewsAnalizer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalizer.DAL.Core
{
    public class NewsAnalizerContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<RssSource> RssSources { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public NewsAnalizerContext(DbContextOptions<NewsAnalizerContext> options) : base(options)
        {

        }
    }
}
