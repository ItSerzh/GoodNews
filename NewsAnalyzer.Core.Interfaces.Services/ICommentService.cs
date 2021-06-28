using NewsAnalyzer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsAnalyzer.Models.View;

namespace NewsAnalyzer.Core.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetCommentsByNewsId(Guid id);
        Task<int> AddComment(CommentDto commentDto);
    }
}
