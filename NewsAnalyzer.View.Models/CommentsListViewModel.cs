using NewsAnalyzer.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAnalyzer.Models.View
{
    public class CommentsListViewModel
    {
        public Guid NewsId { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
    }
}
