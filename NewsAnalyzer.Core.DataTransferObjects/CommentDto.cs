using System;
using System.Collections.Generic;
using System.Text;

namespace NewsAnalyzer.Core.DataTransferObjects
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid NewsId { get; set; }
        public Guid UserId { get; set; }
    }
}
