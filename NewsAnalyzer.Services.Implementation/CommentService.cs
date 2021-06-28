using Microsoft.EntityFrameworkCore;
using NewsAnalyzer.Core.DataTransferObjects;
using NewsAnalyzer.Core.Interfaces.Services;
using NewsAnalyzer.Core.Services.Interfaces;
using NewsAnalyzer.Dal.Repositories.Interfaces;
using NewsAnalyzer.Dal.Repositories.Implementation;
using NewsAnalyzer.DAL.Core;
using NewsAnalyzer.DAL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using NewsAnalyzer.Utils.Html;
using Serilog;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using NewsAnalyzer.Models;
using NewsAnalyzer.Models.View;

namespace NewsAnalyzer.Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> AddComment(CommentDto commentDto)
        {
            await _unitOfWork.Comment.Add(_mapper.Map<Comment>(commentDto));
            return await _unitOfWork.SaveChangesAsync();

        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByNewsId(Guid id)
        {
            var comments = _unitOfWork.Comment.FindBy(c => c.NewsId.Equals(id))
                .Include(c => c.User)
                .OrderBy(c => c.CreateDate);
            return await comments.Select(c => _mapper.Map<CommentDto>(c)).ToListAsync();
        }
    }
}
