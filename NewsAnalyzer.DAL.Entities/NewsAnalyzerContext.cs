using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalyzer.DAL.Core
{
    public class NewsAnalyzerContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<RssSource> RssSources { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public NewsAnalyzerContext(DbContextOptions<NewsAnalyzerContext> options) : base(options)
        {

        }
    }
}
