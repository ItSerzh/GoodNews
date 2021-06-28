using NewsAnalyzer.Dal.Repositories.Interfaces;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Dal.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly NewsAnalyzerContext _db;
        private readonly INewsRepository _newsRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<RssSource> _rssRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;

        public UnitOfWork(NewsAnalyzerContext db, INewsRepository newsRepository, IRepository<RssSource> rssRepository,
            IRepository<User> userRepository, IRepository<Role> roleRepository, IRepository<Comment> commentRepository)
        {
            _db = db;
            _newsRepository = newsRepository;
            _rssRepository = rssRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _commentRepository = commentRepository;
        }

        public INewsRepository News => _newsRepository;
        public IRepository<Comment> Comment => _commentRepository;
        public IRepository<RssSource> RssSource => _rssRepository;
        public IRepository<User> User => _userRepository;
        public IRepository<Role> Role => _roleRepository;

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
        public void Dispose()
        {
            _db?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
